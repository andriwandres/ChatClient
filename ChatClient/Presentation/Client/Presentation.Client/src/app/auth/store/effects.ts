import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ApiError } from '@chat-client/core/models';
import { AuthService } from '@chat-client/core/services';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap, switchMap, tap } from 'rxjs/operators';
import * as authActions from './actions';

@Injectable()
export class AuthEffects {
  readonly authenticate$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.authenticate),
    switchMap(() => {
      // Cancel authentication, when there is no access token
      if (!localStorage.getItem('access_token')) {
        return of(authActions.authenticateFailure({}));
      }

      return this.authService.authenticate().pipe(
        map(user => authActions.authenticateSuccess({ user })),
        catchError((error: ApiError) => of(authActions.authenticateFailure({ error })))
      );
    })
  ));

  readonly navigateToMessenger$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.authenticateSuccess),
    tap(() => {
      this.router.navigateByUrl('');
    })
  ), { dispatch: false });

  readonly createAccount$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.createAccount),
    mergeMap(({ credentials }) => this.authService.createAccount(credentials).pipe(
      map(() => authActions.createAccountSuccess()),
      catchError((error: ApiError) => of(authActions.createAccountFailure({ error })))
    ))
  ));

  readonly login$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.logIn),
    mergeMap(({ credentials }) => this.authService.logIn(credentials).pipe(
      map(user => authActions.logInSuccess({ user })),
      catchError((error: ApiError) => of(authActions.logInFailure({ error })))
    ))
  ));

  readonly emailExists$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.emailExists),
    switchMap(({ email }) => this.authService.emailExists(email).pipe(
      map(result => authActions.emailExistsSuccess({ result })),
      catchError((error: ApiError) => of(authActions.emailExistsFailure({ error })))
    ))
  ));

  readonly userNameExists$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.userNameExists),
    switchMap(({ userName }) => this.authService.userNameExists(userName).pipe(
      map(result => authActions.userNameExistsSuccess({ result })),
      catchError((error: ApiError) => of(authActions.userNameExistsFailure({ error })))
    ))
  ));

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly actions$: Actions<authActions.AuthActionUnion>
  ) {}
}
