import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, switchMap, tap } from 'rxjs/operators';
import { AuthService } from 'src/app/core/auth.service';
import * as authActions from './actions';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class AuthEffects {
  readonly authenticateEffect$ = createEffect(() => this.actions$.pipe(
    ofType(authActions.authenticate),
    exhaustMap(() => this.authService.authenticate().pipe(
      map(user => authActions.authenticateSuccess({ user })),
      tap(() => this.router.navigate([''])),
      catchError(error => of(authActions.authenticateFailure({ error })).pipe(
        tap(() => this.router.navigate['/auth/login'])
      )),
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
      tap(() => {
        this.snackbar.open('Your account has been created', 'Dismiss', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
        });

        this.router.navigate(['/auth/login']);
      }),
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
    private readonly snackbar: MatSnackBar,
    private readonly authService: AuthService,
    private readonly actions$: Actions<authActions.AuthActionUnion>,
  ) {}
}
