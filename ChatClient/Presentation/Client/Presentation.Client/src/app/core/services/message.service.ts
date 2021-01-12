import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ChatMessage } from '../models';

@Injectable({ providedIn: 'root' })
export class MessageService {
  constructor(private readonly httpClient: HttpClient) {}

  getMessages(recipientId: number): Observable<ChatMessage[]> {
    const url = environment.api.recipients + `/${recipientId}/messages`;

    return this.httpClient.get<ChatMessage[]>(url);
  }
}
