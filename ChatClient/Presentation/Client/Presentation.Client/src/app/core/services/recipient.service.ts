import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Recipient } from '../models';

@Injectable({ providedIn: 'root' })
export class RecipientService {
  constructor(private readonly http: HttpClient) {}

  getRecipients(): Observable<Recipient[]> {
    const url = `${environment.api.users}/me/recipients`;

    return this.http.get<Recipient[]>(url);
  }
}
