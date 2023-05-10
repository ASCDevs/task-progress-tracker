import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TasksComponent, NewTaskDialogComponent,MyErrorStateMatcher } from './tasks/tasks.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import {MatButtonModule} from '@angular/material/button';
import { ModalBaseComponent } from './modal-base/modal-base.component';
import {MatDialogModule} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';

const MaterialModules = [
  MatButtonModule,
  MatTableModule,
  MatDialogModule,
  MatInputModule
]

@NgModule({
  declarations: [
    AppComponent,
    TasksComponent,
    ModalBaseComponent,
    NewTaskDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ...MaterialModules 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
