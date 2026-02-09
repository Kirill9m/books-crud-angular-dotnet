import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ErrorMessageService } from '../../service/ErrorMessageService';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-register',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  errorMessage: string = '';
  passwordStrengthPercent: number = 0;
  username: string = '';
  password: string = '';
  repeatPassword: string = '';

  http = inject(HttpClient);
  router = inject(Router);
  auth = inject(AuthService);

  onRegister() {
    if (this.password !== this.repeatPassword) {
      this.errorMessageService.showErrorMessage('Lösenorden matchar inte. Försök igen.');
      return;
    }
    this.http
      .post(
        `${environment.apiBaseUrl}/api/auth/register`,
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
          if (error.status === 400) {
            this.errorMessageService.showErrorMessage(error.error);
          } else {
            this.errorMessageService.showErrorMessage(
              'Registrering misslyckades. Kontrollera användarnamn och lösenord.',
            );
          }
        },
      });
  }

  updatePasswordStrength() {
    this.passwordStrengthPercent = Math.min(this.password.length * 12, 100);
  }

  constructor(public errorMessageService: ErrorMessageService) {}
}
