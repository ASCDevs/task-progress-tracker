import { Component, ViewChild } from '@angular/core';
import { TasksSavedService } from '../services/tasks-saved.service';
import { ITask } from '../ITasks';
import {ITaskSend } from '../ITaskSend';
import * as SignalR from '@microsoft/signalr';
import { API_SIGNAL_HUB } from 'src/environments/environment';
import { MatTable } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import {  FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent {
  displayedColumns: string[] = ['taskName', 'statusUm', 'dtSolicitacao', 'dtInicio','dtFinalizacao'];
  loadTasks!: ITask[];
  nameTaskControl = new FormControl('', [Validators.required]);
  matcher = new MyErrorStateMatcher();

  @ViewChild(MatTable) table!: MatTable<ITask>;

  constructor(private tasksSavedService: TasksSavedService, public dialog: MatDialog){
    this.obterTasksSaved()
    //fazer recurso de atualização automático - deixar botão para ativar/desativar
    //dar refresh na tabela chamando API e não adicionando novos itens via signalr
  }

  ngOnInit(){

    const connection = new SignalR.HubConnectionBuilder()
      .configureLogging(SignalR.LogLevel.Information)
      .withUrl(API_SIGNAL_HUB)
      .build();

    connection.start().then(function(){
      console.log('Hub SignalR connected')
    }).catch(function (err){
      return console.error(err.toString());
    });

    connection.on("updateCountUIs",(connectedUIsCount) =>{
        //console.log(connectedUIsCount)
    })

    connection.on("updateListaTarefas",(tarefas) =>{
        //console.log(tarefas)
    })
    
    connection.on("updateTask",(tarefa: ITask) =>{
      console.info(tarefa.taskName+" - "+tarefa.status);
      let index = this.loadTasks.findIndex(x => x.idTask == tarefa.idTask);
      if(index != -1){
        this.loadTasks[index] = tarefa;
        this.table.renderRows();
      }else{
        this.loadTasks.push(tarefa);
        this.table.renderRows();
      }
      
    })

  }

  obterTasksSaved(){
    this.tasksSavedService.obterTodos()
    .then(tasks => {
      this.loadTasks = [];
      tasks?.forEach(t => this.loadTasks.push(t));
    })
    .catch(error => console.error(error))
  }

  openNewTaskWindow(){
    const dialogRef = this.dialog.open(NewTaskDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`)
    })
  }
  
}

@Component({
  selector: 'dialog-content-example-dialog',
  templateUrl: './newtask-content-dialog.html',
  styleUrls: ['./newtask-contet-dialog.css']
})
export class NewTaskDialogComponent { 
  taskNameFormControl = new FormControl('',[Validators.required]);
  matcher = new MyErrorStateMatcher();

  constructor(private tasksSavedService: TasksSavedService){

  }

  EnviarTarefa(){
    if(this.taskNameFormControl.valid){
      const tarefa: ITaskSend = { NomeTarefa: this.taskNameFormControl.value! };
      this.tasksSavedService.enviarTarefa(tarefa).subscribe(()=>{
        console.log("fechar janela")
      })
    }else{
      console.log("Não está válida o nome da tarefa")
    }
  }
}


