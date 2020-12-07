import { Injectable } from '@angular/core';
import { Action, Store } from '@ngrx/store';
import * as fromAuth from './auth.state';

@Injectable()
export class AuthFacade {
  constructor(private readonly store: Store<fromAuth.AuthPartialState>) {}

  dispatch(action: Action) {
    this.store.dispatch(action);
  }
}
