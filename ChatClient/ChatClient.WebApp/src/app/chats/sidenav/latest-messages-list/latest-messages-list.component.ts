import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';
import { LatestMessagesStoreSelectors, LatestMessagesStoreActions } from 'src/app/app-store/latest-messages-store';
import { takeUntil } from 'rxjs/operators';
import { LatestMessage } from 'src/models/messages/latest-message';

@Component({
  selector: 'app-latest-messages-list',
  templateUrl: './latest-messages-list.component.html',
  styleUrls: ['./latest-messages-list.component.scss']
})
export class LatestMessagesListComponent implements OnInit, OnDestroy {
  private readonly destroy$ = new Subject<void>();

  readonly messages$ = this.store$.pipe(
    select(LatestMessagesStoreSelectors.selectAll),
    takeUntil(this.destroy$),
  );

  readonly activeMessage$ = this.store$.pipe(
    select(LatestMessagesStoreSelectors.selectActiveMessage),
    takeUntil(this.destroy$),
  );

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    this.store$.dispatch(LatestMessagesStoreActions.loadLatestMessages());
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  setActiveMessage(message: LatestMessage): void {
    this.store$.dispatch(LatestMessagesStoreActions.selectActiveMessage({ message }));
  }

  trackByFn(message: LatestMessage, index: number): number {
    return message.messageRecipientId;
  }
}
