import { Component, ViewChild } from '@angular/core';
import { TasksSavedService } from '../services/tasks-saved.service';
import { ITask } from '../ITasks';
import * as SignalR from '@microsoft/signalr';
import { API_SIGNAL_HUB } from 'src/environments/environment';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent {
  displayedColumns: string[] = ['taskName', 'statusUm', 'dtSolicitacao', 'dtInicio','dtFinalizacao'];
  loadTasks!: ITask[];

  @ViewChild(MatTable) table!: MatTable<ITask>;

  constructor(private tasksSavedService: TasksSavedService){
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
    console.log("Abrir modal para nova tarefa")
  }
  
}
