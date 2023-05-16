import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { API_TASKS_SAVED,API_SEND_TASK } from '../../environments/environment';
import { ITask } from '../ITasks';
import { ITaskSend } from '../ITaskSend';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    //Authorization: 'my-auth-token'
  })
}

@Injectable({
  providedIn: 'root'
})
export class TasksSavedService {

  constructor(private httpClient: HttpClient) { }

  obterTodos(){
    return this.httpClient.get<ITask[]>(`${API_TASKS_SAVED}`).toPromise();
  }

  enviarTarefa(tarefa: ITaskSend) : Observable<ITaskSend>{
    console.log("Tarefa a ser enviada:")
    console.log(tarefa)
    
    return this.httpClient.post(`${API_SEND_TASK}`,tarefa)
      .pipe(
        catchError(this.handleError)
      )
  }

  private handleError(error: HttpErrorResponse){
    if(error.status === 0){
      console.error("Erro ocorreu: ",error);
    }else{
      console.error(
        `Backend returned code ${error.status}, body was: `,error.error
      );
    }

    return throwError(()=> new Error('Something bad happened; please try again later.'));
  }
}

