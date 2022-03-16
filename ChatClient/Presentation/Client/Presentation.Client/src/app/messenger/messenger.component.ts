import { Component, OnInit } from '@angular/core';
import { WebSocketFacade } from '../shared/store/websocket/websocket.facade';
import { RecipientFacade } from '../shared/store/recipients';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {
  readonly selectedRecipient$ = this.recipientFacade.selectedRecipient$;

  constructor(
    private readonly recipientFacade: RecipientFacade,
    private readonly webSocketFacade: WebSocketFacade,
  ) { }

  ngOnInit(): void {
    this.webSocketFacade.connectWebSocket();
  }
}
