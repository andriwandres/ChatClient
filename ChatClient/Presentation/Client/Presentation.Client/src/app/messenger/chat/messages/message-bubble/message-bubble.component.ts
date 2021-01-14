import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { ChatMessage } from '@chat-client/core/models';

@Component({
  selector: 'app-message-bubble',
  templateUrl: './message-bubble.component.html',
  styleUrls: ['./message-bubble.component.scss'],
})
export class MessageBubbleComponent implements OnInit {
  @Input() message!: ChatMessage;

  @HostBinding('style.flex-direction') flexDirection = 'row';

  constructor() {}

  ngOnInit(): void {
    if (this.message.isOwnMessage) {
      this.flexDirection = 'row-reverse';
    }
  }
}
