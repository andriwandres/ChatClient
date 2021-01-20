import { Component, Input } from '@angular/core';
import { Recipient } from '@chat-client/core/models';
import { MessageFacade } from './messages/store';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  @Input() recipient!: Recipient;

  constructor(private readonly messageFacade: MessageFacade) {}

  onMessageSent(message: string): void {
    this.messageFacade.sendMessage({
      recipientId: this.recipient.recipientId,
      htmlContent: message,
    });
  }
}
