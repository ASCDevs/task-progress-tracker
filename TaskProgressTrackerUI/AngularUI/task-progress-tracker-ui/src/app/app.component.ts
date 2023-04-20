import { Component } from '@angular/core';
import { TasksSavedService } from './tasks-saved.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Task Progress Tracker';
  subtitle = "An app to order and follow tasks in progress.";
}
