import { Component } from '@angular/core';

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
  displayedColumns: string[] = ['nome', 'status', 'dtPedido', 'dtInicio','dtFinal'];
  dataSource = ELEMENT_DATA;
}
