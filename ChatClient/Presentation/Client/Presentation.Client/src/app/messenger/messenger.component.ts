import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RecipientFacade } from './sidenav/recipients/store';
import { MessengerFacade } from './store';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {
  readonly selectedRecipient$ = this.recipientFacade.selectedRecipient$;

  constructor(
    private readonly recipientFacade: RecipientFacade,
    private readonly messengerFacade: MessengerFacade,
  ) { }

  ngOnInit(): void {
    this.messengerFacade.connectWebSocket();
  }
}
