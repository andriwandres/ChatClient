import { Component, OnInit } from '@angular/core';
import { AuthFacade } from './shared/store/auth';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  readonly authenticationAttempted$ = this.authFacade.authenticationAttempted$;

  constructor(private readonly authFacade: AuthFacade) {}

  ngOnInit(): void {
    this.authFacade.authenticate();
  }
}
