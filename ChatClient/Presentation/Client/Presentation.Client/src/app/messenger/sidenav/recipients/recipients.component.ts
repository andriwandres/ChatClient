import { Component, OnInit } from '@angular/core';
import { Recipient } from '@chat-client/core/models';
import { RecipientFacade } from './store';

@Component({
  selector: 'app-recipients',
  templateUrl: './recipients.component.html',
  styleUrls: ['./recipients.component.scss']
})
export class RecipientsComponent implements OnInit {
  readonly recipients$ = this.recipientFacade.recipients$;

  constructor(private readonly recipientFacade: RecipientFacade) { }

  ngOnInit(): void {
    this.recipientFacade.loadRecipients();
  }

  selectRecipient(recipient: Recipient): void {
    this.recipientFacade.selectRecipient(recipient);
  }
}
