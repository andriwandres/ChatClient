import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ChatMessage, ChatMessageDto } from 'src/models/messages/chat-message';

@Injectable({ providedIn: 'root' })
export class ChatMessagesService {
  constructor(private readonly http: HttpClient) { }

  getPrivateMessages(recipientId: number): Observable<ChatMessage[]> {
    const url = `${environment.api.message}/GetPrivateMessages/${recipientId}`;

    return this.http.get<ChatMessage[]>(url).pipe(
      retry(1),
    );
  }

  getGroupMessages(groupId: number): Observable<ChatMessage[]> {
    const url = `${environment.api.message}/GetGroupMessages/${groupId}`;

    return this.http.get<ChatMessage[]>(url).pipe(
      retry(1)
    );
  }

  addPrivateMessage(message: ChatMessageDto): Observable<ChatMessage> {
    const url = `${environment.api.message}/AddPrivateMessage`;

    return this.http.post<ChatMessage>(url, message);
  }

  addGroupMessage(message: ChatMessageDto): Observable<ChatMessage> {
    const url = `${environment.api.message}/AddGroupMessage`;

    return this.http.post<ChatMessage>(url, message);
  }
}
