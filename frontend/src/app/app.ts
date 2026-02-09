import { Component, DOCUMENT, inject, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCloudSun, faInfo, faUser, faUserSlash } from '@fortawesome/free-solid-svg-icons';
import { FormsModule } from '@angular/forms';
import { NgStyle } from '@angular/common';
import { AuthService } from './auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [FontAwesomeModule, RouterLink, RouterOutlet, FormsModule, NgStyle],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  private readonly document = inject(DOCUMENT);
  faBulb = faCloudSun;
  infoBtn = faInfo;
  userLogo = faUser;
  logoutLogo = faUserSlash;
  auth = inject(AuthService);
  isThemeDark: boolean = false;
  hintClass: string = '';

  toggleTheme() {
    const html = this.document.documentElement;
    const current = html.getAttribute('data-bs-theme') ?? 'light';
    const newTheme = current === 'dark' ? 'light' : 'dark';
    html.setAttribute('data-bs-theme', newTheme);
    this.isThemeDark = newTheme === 'dark';
    localStorage.setItem('theme', newTheme);
  }

  showHint() {
    this.hintClass = 'text-warning';
    setTimeout(() => {
      this.hintClass = '';
    }, 3000);
  }

  ngOnInit(): void {
    this.isThemeDark = this.document.documentElement.getAttribute('data-bs-theme') === 'dark';
    if (localStorage.getItem('token')) {
      this.auth.refreshMe().subscribe();
    }

    const storedTheme = localStorage.getItem('theme');
    if (storedTheme) {
      this.document.documentElement.setAttribute('data-bs-theme', storedTheme);
      this.isThemeDark = storedTheme === 'dark';
    }
  }
}
