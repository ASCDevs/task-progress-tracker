import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TasksComponent } from './tasks/tasks.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import {MatButtonModule} from '@angular/material/button';
import { ModalBaseComponent } from './modal-base/modal-base.component';

const MaterialComponents = [
  MatButtonModule,
  MatTableModule
]

@NgModule({
  declarations: [
    AppComponent,
    TasksComponent,
    ModalBaseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ...MaterialComponents 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
