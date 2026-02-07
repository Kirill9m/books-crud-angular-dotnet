import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  errorMessage: string = '';
  symbols: string = '';
  email: string = '';
  password: string = '';
  repeatPassword: string = '';

  onRegister() {
    this.showErrorMessage('Funktion och implementation saknas');
  }

  showErrorMessage(message: string) {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 3000);
  }

  updateSymbols() {
    if (this.password.length > 0) {
      this.symbols = (this.password.length * 12).toString();
    } else {
      this.symbols = '';
    }
  }
}
