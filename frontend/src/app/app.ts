import { Component, DOCUMENT, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faLightbulb } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';
import { NgStyle } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [FontAwesomeModule, RouterLink, RouterOutlet, FormsModule, NgStyle],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App implements OnInit {
  private readonly document = inject(DOCUMENT);
  faBulb = faLightbulb;
  auth = inject(AuthService);

  toggleTheme() {
    const html = this.document.documentElement;
    const current = html.getAttribute('data-bs-theme') ?? 'light';
    html.setAttribute('data-bs-theme', current === 'dark' ? 'light' : 'dark');
  }
  isThemeDark: boolean = true;

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      this.auth.refreshMe().subscribe();
    }
  }
}
