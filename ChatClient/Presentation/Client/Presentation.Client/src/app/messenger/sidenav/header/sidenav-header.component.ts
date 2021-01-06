import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-sidenav-header',
  templateUrl: './sidenav-header.component.html',
  styleUrls: ['./sidenav-header.component.scss']
})
export class SidenavHeaderComponent {
  @Output() hamburgerMenuClick = new EventEmitter();
}
