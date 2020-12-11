import { Component, OnInit } from '@angular/core';
import { AuthFacade } from '@chat-client/auth/store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  readonly isAuthenticating$ = this.authFacade.isAuthenticating$;

  constructor(private readonly authFacade: AuthFacade) {}

  ngOnInit(): void {
    this.authFacade.authenticate();
  }
}
