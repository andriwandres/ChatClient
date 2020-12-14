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
        return of(authActions.authenticateFailure({ error: null }));
      }

      return this.authService.authenticate().pipe(
        map(user => !!user ? authActions.authenticateSuccess({ user }) : authActions.authenticateFailure({ error: null })),
        catchError((error: ApiError) => of(authActions.authenticateFailure({ error })))
      );
    })
  ));

  readonly authenticateSuccess$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.authenticateSuccess),
    tap(({ user }) => user && localStorage.setItem('access_token', user.token))
  ), {
    dispatch: false
  });

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
      map(user => !!user ? authActions.logInSuccess({ user }) : authActions.logInFailure({ error: null })),
      catchError((error: ApiError) => of(authActions.logInFailure({ error })))
    ))
  ));

  readonly logInSuccess$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.logInSuccess),
    tap(({ user }) => localStorage.setItem('access_token', user.token))
  ), {
    dispatch: false
  });

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

  readonly logOut$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.logOut),
    tap(() => localStorage.removeItem('access_token'))
  ), {
    dispatch: false
  });

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly actions$: Actions<authActions.AuthActionUnion>
  ) {}
}
