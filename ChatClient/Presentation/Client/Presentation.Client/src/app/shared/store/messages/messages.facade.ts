import { Injectable } from '@angular/core';
import { ChatMessage, SendMessageBody } from '@chat-client/core/models';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import * as messageActions from './messages.actions';
import * as messageSelectors from './messages.selectors';

@Injectable({ providedIn: 'root' })
export class MessageFacade {
  readonly messages$ = this.store.select(messageSelectors.selectMessages);
  readonly isLoadingMessages$ = this.store.select(messageSelectors.selectIsLoadingMessages);

  constructor(private readonly store: Store<any>) {}

  selectMessages(recipientId: number): Observable<ChatMessage[]> {
    const selector = messageSelectors.selectChatMessages(recipientId);

    return this.store.select(selector);
  }

  loadMessages(recipientId: number): void {
    this.store.dispatch(messageActions.loadMessages({ recipientId }));
  }

  sendMessage(body: SendMessageBody): void {
    this.store.dispatch(messageActions.sendMessage({ body }));
  }
}
