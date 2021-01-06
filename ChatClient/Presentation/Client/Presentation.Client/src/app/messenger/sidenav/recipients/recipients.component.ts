import { Component, OnInit } from '@angular/core';
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
}
