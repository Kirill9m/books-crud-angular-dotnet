import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Router, NavigationStart } from '@angular/router';
import { filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ErrorMessageService {
  private _message$ = new BehaviorSubject<string>('');
  message$ = this._message$.asObservable();

  constructor(private router: Router) {
    this.router.events.pipe(filter((e) => e instanceof NavigationStart)).subscribe(() => {
      this.clear();
    });
  }

  showErrorMessage(message: string) {
    this._message$.next(message);
    setTimeout(() => {
      this.clear();
    }, 3000);
  }

  clear() {
    this._message$.next('');
  }
}
