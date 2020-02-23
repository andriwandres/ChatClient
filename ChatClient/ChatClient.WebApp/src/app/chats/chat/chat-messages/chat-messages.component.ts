import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AppStoreState } from 'src/app/app-store';
import { ChatMessagesStoreActions, ChatMessagesStoreSelectors } from 'src/app/app-store/chat-messages-store';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.scss']
})
export class ChatMessagesComponent implements OnInit, OnDestroy {
  @Input() targetId: number;
  @Input() isGroupChat: boolean;

  readonly destroy$ = new Subject<void>();

  readonly messages$ = this.store$.pipe(
    select(ChatMessagesStoreSelectors.selectAll),
    takeUntil(this.destroy$)
  );

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    // const action = this.isGroupChat
    //   ? ChatMessagesStoreActions.loadGroupMessages({ groupId: this.targetId })
    //   : ChatMessagesStoreActions.loadPrivateMessages({ recipientId: this.targetId });

    // this.store$.dispatch(action);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
