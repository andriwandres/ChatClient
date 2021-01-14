import { Component, Input } from '@angular/core';
import { Recipient } from '@chat-client/core/models';

@Component({
  selector: 'app-recipient',
  templateUrl: './recipient.component.html',
  styleUrls: ['./recipient.component.scss'],
})
export class RecipientComponent {
  @Input() recipient!: Recipient;

  // truncate(text: string, characters: number): string {
  //   return text.length > characters
  //     ? text.substring(0, characters - 1) + '...'
  //     : text;
  // }
}
