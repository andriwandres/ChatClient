import { Component, OnInit } from '@angular/core';
import { MessengerFacade } from './store';

@Component({
  selector: 'app-messenger',
  templateUrl: './messenger.component.html',
  styleUrls: ['./messenger.component.scss']
})
export class MessengerComponent implements OnInit {
  constructor(private readonly messengerFacade: MessengerFacade) { }

  ngOnInit(): void {
    // this.messengerFacade.connectWebSocket();
  }
}
