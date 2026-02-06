import { Routes } from '@angular/router';
import { Login } from './components/login/login';
import { Bookslist } from './components/bookslist/bookslist';
import { QuotesList } from './components/quotes-list/quotes-list';

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
    component: QuotesList,
  },
];
