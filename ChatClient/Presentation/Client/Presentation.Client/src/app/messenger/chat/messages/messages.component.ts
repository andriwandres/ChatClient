import {
  ChangeDetectionStrategy,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { MessageFacade } from 'src/app/shared/store/messages';
import { RecipientFacade } from 'src/app/shared/store/recipients';

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
    // Load messages
    this.recipientFacade.selectedRecipientId$
      .pipe(takeUntil(this.destroy$))
      .subscribe((recipientId) => {
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
