import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ChatMessage, MessageBoundaries, SendMessageBody } from '../models';

@Injectable({ providedIn: 'root' })
export class MessageService {
  constructor(private readonly httpClient: HttpClient) {}

  getMessages(recipientId: number, boundaries: MessageBoundaries): Observable<ChatMessage[]> {
    const url = environment.api.recipients + `/${recipientId}/messages`;

    let params = new HttpParams()
      .append('limit', boundaries.limit.toString());

    if (boundaries.before) {
      params = params.append('before', boundaries.before.toString());
    }

    const options = { params };

    return this.httpClient.get<ChatMessage[]>(url, options);
  }

  sendMessage(message: SendMessageBody): Observable<ChatMessage> {
    const url = environment.api.messages;

    return this.httpClient.post<ChatMessage>(url, message);
  }
}
