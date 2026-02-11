import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';

export type MeDto = { username: string };

@Injectable({ providedIn: 'root' })
export class AuthService {
  isAuthed = signal(false);
  me = signal<MeDto | null>(null);

  constructor(
    private http: HttpClient,
    private router: Router,
  ) {}

  refreshMe() {
    return this.http.get<MeDto>(`${environment.apiBaseUrl}/api/auth/me`).pipe(
      tap((user) => {
        this.me.set(user);
        this.isAuthed.set(true);
      }),
      map(() => true),
      catchError(() => {
        this.me.set(null);
        this.isAuthed.set(false);
        return of(false);
      }),
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.me.set(null);
    this.isAuthed.set(false);
    this.router.navigate(['/login']).then(() => window.location.reload());
  }
}
