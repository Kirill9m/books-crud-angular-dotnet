import { CommonModule, AsyncPipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ErrorMessageService } from '../../service/ErrorMessageService';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule, CommonModule, AsyncPipe],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  errorMessage: string = '';
  username: string = '';
  password: string = '';

  http = inject(HttpClient);
  router = inject(Router);
  auth = inject(AuthService);

  onLogin() {
    this.http
      .post(
        `${environment.apiBaseUrl}/api/auth/login`,
        {
          username: this.username,
          password: this.password,
        },
        { responseType: 'text' },
      )
      .subscribe({
        next: (response: string) => {
          localStorage.setItem('token', response);
          this.auth.refreshMe().subscribe(() => {
            this.router.navigate(['/']);
          });
        },
        error: (error) => {
          this.errorMessageService.showErrorMessage(error.error);
        },
      });
  }

  constructor(public errorMessageService: ErrorMessageService) {}
}
