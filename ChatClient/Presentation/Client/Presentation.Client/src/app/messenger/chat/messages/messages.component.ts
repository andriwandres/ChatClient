import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';
import { Subject } from 'rxjs';
import { filter, takeUntil, withLatestFrom } from 'rxjs/operators';
import { RecipientFacade } from '../../sidenav/recipients/store';
import { MessageFacade } from './store/facade';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessagesComponent implements OnInit, OnDestroy {
  readonly messages$ = this.messageFacade.messages$;

  private readonly destroy$ = new Subject();

  constructor(
    private readonly messageFacade: MessageFacade,
    private readonly recipientFacade: RecipientFacade
  ) {}

  ngOnInit(): void {
    this.recipientFacade.selectedRecipientId$
      .pipe(
        takeUntil(this.destroy$),
        withLatestFrom(this.messages$),
        filter(([, messages]) => !messages || messages.length === 0)
      )
      .subscribe(([recipientId]) => {
        if (recipientId) {
          this.messageFacade.loadMessages(recipientId);
        }
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
