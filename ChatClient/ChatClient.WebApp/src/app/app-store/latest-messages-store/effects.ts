import { Injectable } from '@angular/core';
import { Actions, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { LatestMessagesService } from 'src/app/core/latest-messages.service';
import * as latestMessageActions from './actions';

@Injectable()
export class LatestMessageEffects {
  readonly loadLatestMessagesEffect$ = this.actions$.pipe(
    ofType(latestMessageActions.loadLatestMessages),
    switchMap(() => this.latestMessageService.getLatestMessages().pipe(
      map(latestMessages => latestMessageActions.loadLatestMessagesSuccess({ latestMessages })),
      catchError(error => of(latestMessageActions.loadLatestMessagesFailure({ error }))),
    )),
  );

  constructor(
    private readonly latestMessageService: LatestMessagesService,
    private readonly actions$: Actions<latestMessageActions.LatestMessagesUnion>,
  ) {}
}
