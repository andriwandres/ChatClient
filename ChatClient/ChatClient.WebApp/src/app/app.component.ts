import { Component, OnInit } from '@angular/core';
import { AuthStoreActions } from './app-store/auth-store';
import { AppStoreState } from './app-store';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    this.store$.dispatch(AuthStoreActions.authenticate());
  }
}
