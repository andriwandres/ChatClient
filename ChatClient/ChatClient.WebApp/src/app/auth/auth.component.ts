import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppStoreState } from '../app-store';
import { AuthStoreActions } from '../app-store/auth-store';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    this.store$.dispatch(AuthStoreActions.authenticate());
  }
}
