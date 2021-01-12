import { Component, OnInit } from '@angular/core';
import { RecipientFacade } from './sidenav/recipients/store';
import { WebSocketFacade } from './store';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {
  readonly selectedRecipient$ = this.recipientFacade.selectedRecipient$;

  constructor(
    private readonly recipientFacade: RecipientFacade,
    private readonly messengerFacade: WebSocketFacade,
  ) { }

  ngOnInit(): void {
    this.messengerFacade.connectWebSocket();
  }
}
