import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-sidenav-header',
  templateUrl: './sidenav-header.component.html',
  styleUrls: ['./sidenav-header.component.scss']
})
export class SidenavHeaderComponent {
  @Output() readonly addClick = new EventEmitter<void>(true);
  @Output() readonly menuClick = new EventEmitter<void>(true);

  readonly searchControl = new FormControl('');
}
