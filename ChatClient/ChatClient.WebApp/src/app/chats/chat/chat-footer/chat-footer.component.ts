import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-chat-footer',
  templateUrl: './chat-footer.component.html',
  styleUrls: ['./chat-footer.component.scss']
})
export class ChatFooterComponent {
  @Output() attachFile = new EventEmitter<void>();
  @Output() sendMessage = new EventEmitter<string>();

  readonly messageControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/.*[^\s].*/),
  ]);
}
