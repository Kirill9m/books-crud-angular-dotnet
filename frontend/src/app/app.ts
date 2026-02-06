import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Bookslist } from './components/bookslist/bookslist';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-root',
  imports: [Bookslist, FontAwesomeModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
