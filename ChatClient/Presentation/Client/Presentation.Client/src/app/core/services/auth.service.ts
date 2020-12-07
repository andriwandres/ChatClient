import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticatedUser } from '@core/models';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private readonly httpClient: HttpClient) { }

  authenticateUser(): Observable<AuthenticatedUser> {
    const url = `${environment.api.user}/me`;

    return this.httpClient.get<AuthenticatedUser>(url);
  }
}
