import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-contacts-header',
  templateUrl: './contacts-header.component.html',
  styleUrls: ['./contacts-header.component.scss']
})
export class ContactsHeaderComponent {
  @Output() readonly addClick = new EventEmitter<void>(true);
  @Output() readonly menuClick = new EventEmitter<void>(true);

  readonly searchControl = new FormControl('');
}
