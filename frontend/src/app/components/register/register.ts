import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ErrorMessageService } from '../../service/ErrorMessageService';

@Component({
  selector: 'app-register',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  errorMessage: string = '';
  passwordStrengthPercent: number = 0;
  email: string = '';
  password: string = '';
  repeatPassword: string = '';

  onRegister() {
    this.errorMessageService.showErrorMessage('Funktion och implementation saknas');
  }

  updatePasswordStrength() {
    this.passwordStrengthPercent = Math.min(this.password.length * 12, 100);
  }

  constructor(public errorMessageService: ErrorMessageService) {}
}
