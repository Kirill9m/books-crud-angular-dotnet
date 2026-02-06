import { Component, DOCUMENT, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faLightbulb } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';
import { DatePipe, NgStyle, TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [FontAwesomeModule, RouterLink, RouterOutlet, FontAwesomeModule, FormsModule, NgStyle],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private readonly document = inject(DOCUMENT);
  faBulb = faLightbulb;

  toggleTheme() {
    const html = this.document.documentElement;
    const current = html.getAttribute('data-bs-theme') ?? 'light';
    html.setAttribute('data-bs-theme', current === 'dark' ? 'light' : 'dark');
  }
  ngOnInit(): void {
    console.log('App initialized');
  }
  isThemeDark: boolean = true;
}
