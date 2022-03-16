import { Injectable } from '@angular/core';
import { Recipient } from '@chat-client/core/models';
import { Store } from '@ngrx/store';
import * as recipientActions from './recipients.actions';
import * as recipientSelectors from './recipients.selectors';
import { PartialState } from './recipients.state';

@Injectable({ providedIn: 'root' })
export class RecipientFacade {
  readonly recipients$ = this.store.select(recipientSelectors.selectAll);
  readonly selectedRecipient$ = this.store.select(recipientSelectors.selectSelectedRecipient);
  readonly selectedRecipientId$ = this.store.select(recipientSelectors.selectSelectedRecipientId);

  readonly isLoadingRecipients$ = this.store.select(recipientSelectors.selectIsLoadingRecipients);

  constructor(private readonly store: Store<PartialState>) {}

  loadRecipients(): void {
    this.store.dispatch(recipientActions.loadRecipients());
  }

  selectRecipient(recipient: Recipient): void {
    this.store.dispatch(recipientActions.selectRecipient({ recipient }));
  }
}
