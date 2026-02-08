import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Bookslist } from './components/bookslist/bookslist';
import { QuotesList } from './components/quotes-list/quotes-list';
import { Register } from './components/register/register';
import { authGuard } from './auth/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: '',
    component: Bookslist,
  },
  {
    path: 'quotes',
    canActivate: [authGuard],
    component: QuotesList,
  },
  {
    path: 'register',
    component: Register,
  },
];
