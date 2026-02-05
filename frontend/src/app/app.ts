import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Bookslist } from './components/bookslist/bookslist';

@Component({
  selector: 'app-root',
  imports: [Bookslist],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
