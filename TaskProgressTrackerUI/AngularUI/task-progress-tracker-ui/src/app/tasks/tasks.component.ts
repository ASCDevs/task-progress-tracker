import { Component } from '@angular/core';
import { TasksSavedService } from '../tasks-saved.service';
import { ITask } from '../ITasks';
import * as SignalR from '@microsoft/signalr';
import { API_SIGNAL_HUB } from 'src/environments/environment';

export interface Tarefa{
  nomeTarefa: string;
  status: string;
  dtPedido?: string;
  dtInicio?: string;
  dtFinal?: string;
}

const ELEMENT_DATA: Tarefa[] = [
  { nomeTarefa: "Sushi", status: "Solicitado",dtPedido:"17/04/2023 21:15",dtInicio:"-",dtFinal:"-"},
  { nomeTarefa: "HotRoll", status: "Concluído",dtPedido:"14/04/2023 21:15",dtInicio:"14/04/2023 21:30",dtFinal:"14/04/2023 22:30"},
  { nomeTarefa: "Niguiri", status: "Preparando - fase 1",dtPedido:"09/04/2023 19:15",dtInicio:"09/04/2023 19:20",dtFinal:"-"},
  { nomeTarefa: "Uramaki", status: "Preparando - fase 4",dtPedido:"01/04/2023 16:15",dtInicio:"01/04/2023 16:42",dtFinal:"-"},
];

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent {
  displayedColumns: string[] = ['nome', 'status', 'pedidoTarefa', 'inicioTarefa','fimTarefa'];
  loadTasks!: ITask[];

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

    connection.on("addMostRecentTask",(tarefa) =>{
        console.log(tarefa)
    })
    
    connection.on("updateTask",(tarefa) =>{
        console.log(tarefa)
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
}
