import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';
import { AuthenticatedUser } from 'src/models/auth/user';
import { LoginDto } from 'src/models/auth/login';
import { environment } from 'src/environments/environment';
import { RegisterDto } from 'src/models/auth/register';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private readonly http: HttpClient) {}

  authenticate(): Observable<AuthenticatedUser> {
    const url = `${environment.api.auth}/Authenticate`;

    return this.http.get<AuthenticatedUser>(url);
  }

  login(credentials: LoginDto): Observable<AuthenticatedUser> {
    const url = `${environment.api.auth}/Login`;

    return this.http.post<AuthenticatedUser>(url, credentials);
  }

  register(credentials: RegisterDto): Observable<void> {
    const url = `${environment.api.auth}/Register`;

    return this.http.post<void>(url, credentials);
  }

  checkEmailTaken(email: string): Observable<boolean> {
    const url = `${environment.api.auth}/IsEmailTaken`;
    const options = { params: new HttpParams().set('email', email) };

    return this.http.get<boolean>(url, options).pipe(
      retry(2),
    );
  }
}
