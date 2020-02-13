import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, switchMap, tap } from 'rxjs/operators';
import { AuthService } from 'src/app/core/auth.service';
import * as authActions from './actions';
import { Router } from '@angular/router';

@Injectable()
export class AuthEffects {
  readonly authenticateEffect$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.authenticate),
    exhaustMap(() => this.authService.authenticate().pipe(
      map(user => authActions.authenticateSuccess({ user })),
      catchError(error => of(authActions.authenticateFailure({ error }))),
    )),
  ));

  readonly loginEffect$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.login),
    exhaustMap(action => this.authService.login(action.credentials).pipe(
      map(user => authActions.loginSuccess({ user })),
      tap(() => this.router.navigate([''])),
      catchError(error => of(authActions.loginFailure({ error })))
    ))
  ));

  readonly registerEffect$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.register),
    switchMap(action => this.authService.register(action.credentials).pipe(
      map(() => authActions.registerSuccess()),
      catchError(error => of(authActions.registerFailure({ error }))),
    )),
  ));

  readonly checkEmailAvailability = createEffect(() => this.actions$.pipe(
    ofType(authActions.checkEmailAvailability),
    switchMap(action => this.authService.checkEmailTaken(action.email).pipe(
      map(result => authActions.checkEmailAvailabilitySuccess({ result })),
      catchError(error => of(authActions.checkEmailAvailabilityFailure({ error }))),
    )),
  ));

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly actions$: Actions<authActions.AuthActionUnion>,
  ) {}
}
