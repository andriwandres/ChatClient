import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiError } from '@chat-client/core/models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { RecipientService } from 'src/app/core/services/recipient.service';
import * as recipientActions from './actions';

@Injectable()
export class RecipientEffects {
  readonly loadRecipients$ = createEffect(() => this.actions$.pipe(
    ofType(recipientActions.loadRecipients),
    switchMap(() => this.recipientService.getRecipients().pipe(
      map(recipients => recipientActions.loadRecipientsSuccess({ recipients })),
      catchError((response: HttpErrorResponse) => {
        return of(recipientActions.loadRecipientsFailure({ error: response.error as ApiError }));
      })
    ))
  ));

  constructor(
    private readonly actions$: Actions,
    private readonly recipientService: RecipientService
  ) {}
}
