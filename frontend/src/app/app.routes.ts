import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Bookslist } from './components/bookslist/bookslist';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'books',
    component: Bookslist,
  },
];
