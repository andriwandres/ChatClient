import { Component, OnInit, Input, HostBinding } from '@angular/core';
import { ChatMessage } from 'src/models/messages/chat-message';

@Component({
  selector: 'app-chat-bubble',
  templateUrl: './chat-bubble.component.html',
  styleUrls: ['./chat-bubble.component.scss']
})
export class ChatBubbleComponent implements OnInit {
  @Input() message: ChatMessage;
  @Input() isAnchor = false;

  @HostBinding('style.flex-direction') flexDirection: string;

  constructor() { }

  ngOnInit(): void {
    this.flexDirection = this.message.isOwnMessage ? 'row-reverse' : 'row';
  }
}
