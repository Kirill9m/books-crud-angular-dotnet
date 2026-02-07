import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ErrorMessageService } from '../../service/ErrorMessageService';

@Component({
  selector: 'app-login',
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  errorMessage: string = '';
  email: string = '';
  password: string = '';

  onLogin() {
    this.errorMessageService.showErrorMessage('Funktion och implementation saknas');
  }

  constructor(public errorMessageService: ErrorMessageService) {}
}
