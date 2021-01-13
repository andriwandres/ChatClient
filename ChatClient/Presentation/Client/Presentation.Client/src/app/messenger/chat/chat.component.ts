import { Component, Input } from '@angular/core';
import { Recipient } from '@chat-client/core/models';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  @Input() recipient!: Recipient;
}
