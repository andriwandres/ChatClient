import { Store } from '@ngrx/store';
import * as recipientActions from './actions';
import * as recipientSelectors from './selectors';
import { PartialState } from './state';

export class RecipientFacade {
  readonly recipients$ = this.store.select(recipientSelectors.selectAll);
  readonly isLoadingRecipients$ = this.store.select(recipientSelectors.selectIsLoadingRecipients);

  constructor(private readonly store: Store<PartialState>) {}

  loadRecipients(): void {
    this.store.dispatch(recipientActions.loadRecipients());
  }
}
