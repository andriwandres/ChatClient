import { Injectable } from '@angular/core';
import { CreateAccountCredentials, LoginCredentials } from '@chat-client/core/models';
import { Store } from '@ngrx/store';
import * as authActions from './actions';
import * as authSelectors from './selectors';
import { PartialState } from './state';

@Injectable({ providedIn: 'root' })
export class AuthFacade {
  readonly user$ = this.store.select(authSelectors.selectUser);
  readonly token$ = this.store.select(authSelectors.selectToken);

  readonly emailExists$ = this.store.select(authSelectors.selectEmailExists);
  readonly userNameExists$ = this.store.select(authSelectors.selectUserNameExists);

  readonly isAuthenticating$ = this.store.select(authSelectors.selectIsAuthenticating);
  readonly error$ = this.store.select(authSelectors.selectError);

  constructor(private readonly store: Store<PartialState>) {}

  authenticate(): void {
    this.store.dispatch(authActions.authenticate());
  }

  logIn(credentials: LoginCredentials): void {
    this.store.dispatch(authActions.logIn({ credentials }));
  }

  logOut(): void {
    this.store.dispatch(authActions.logOut());
  }

  createAccount(credentials: CreateAccountCredentials): void {
    this.store.dispatch(authActions.createAccount({ credentials }));
  }

  checkEmailExists(email: string): void {
    this.store.dispatch(authActions.emailExists({ email }));
  }

  checkUserNameExists(userName: string): void {
    this.store.dispatch(authActions.userNameExists({ userName }));
  }

  resetAvailabilityChecks(): void {
    this.store.dispatch(authActions.resetAvailabilityChecks());
  }
}
