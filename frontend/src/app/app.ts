import { Component, DOCUMENT, inject } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faLightbulb } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';
import { NgStyle } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [FontAwesomeModule, RouterLink, RouterOutlet, FontAwesomeModule, FormsModule, NgStyle],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  private readonly document = inject(DOCUMENT);
  faBulb = faLightbulb;

  toggleTheme() {
    const html = this.document.documentElement;
    const current = html.getAttribute('data-bs-theme') ?? 'light';
    html.setAttribute('data-bs-theme', current === 'dark' ? 'light' : 'dark');
    this.isThemeDark = !this.isThemeDark;
  }

  isThemeDark: boolean = true;
}

