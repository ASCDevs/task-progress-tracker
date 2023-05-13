import { Component, ViewChild } from '@angular/core';
import { TasksSavedService } from '../services/tasks-saved.service';
import { ITask } from '../ITasks';
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
        console.log(connectedUIsCount)
    })

    connection.on("updateListaTarefas",(tarefas) =>{
        console.log(tarefas)
    })
    
    connection.on("updateTask",(tarefa: ITask) =>{
      console.log("Tarefa update: ")
      console.info(tarefa.taskName+" - "+tarefa.status);
      let index = this.loadTasks.findIndex(x => x.idTask == tarefa.idTask);
      if(index != -1){
        console.log("Index: "+index);
        //console.log("Antes: "+this.loadTasks[index].taskName+" - "+this.loadTasks[index].status);
        this.loadTasks[index] = tarefa;
        //console.log("Depois: "+this.loadTasks[index].taskName+" - "+this.loadTasks[index].status);
        console.log(this.loadTasks)
        this.table.renderRows();
      }else{
        console.log("Tarefa nova");
        console.info(tarefa.taskName+" - "+tarefa.status);

        this.loadTasks.push(tarefa);
        console.log(this.loadTasks)
        this.table.renderRows();
      }
      
    })

  }

  obterTasksSaved(){
    this.tasksSavedService.obterTodos()
    .then(tasks => {
      this.loadTasks = [];
      tasks?.forEach(t => this.loadTasks.push(t));
      console.log(this.loadTasks);
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

  EnviarTarefa(){
    console.log(this.taskNameFormControl.value)
  }
}


