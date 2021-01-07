import { Component, Input, OnInit } from '@angular/core';
import { Recipient } from '@chat-client/core/models';

@Component({
  selector: 'app-chat-header',
  templateUrl: './chat-header.component.html',
  styleUrls: ['./chat-header.component.scss']
})
export class ChatHeaderComponent implements OnInit {
  @Input() recipient!: Recipient;

  constructor() { }

  ngOnInit(): void {
  }

}
