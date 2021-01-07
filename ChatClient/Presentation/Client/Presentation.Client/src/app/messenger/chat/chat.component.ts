import { Component, Input, OnInit } from '@angular/core';
import { Recipient } from '@chat-client/core/models';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  @Input() recipient!: Recipient;

  constructor() { }

  ngOnInit(): void {
  }

}
