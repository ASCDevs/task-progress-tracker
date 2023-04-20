import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_TASKS_SAVED } from '../environments/environment';
import { ITask } from './ITasks';

@Injectable({
  providedIn: 'root'
})
export class TasksSavedService {

  constructor(private httpClient: HttpClient) { }

  obterTodos(){
    return this.httpClient.get<ITask[]>(`${API_TASKS_SAVED}`).toPromise();
  }
}
