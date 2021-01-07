import { Component, Input } from '@angular/core';
import { Recipient } from '@chat-client/core/models';

@Component({
  selector: 'app-recipient',
  templateUrl: './recipient.component.html',
  styleUrls: ['./recipient.component.scss']
})
export class RecipientComponent {
  @Input() recipient!: Recipient;
}
