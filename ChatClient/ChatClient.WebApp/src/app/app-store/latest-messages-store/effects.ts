import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { LatestMessagesService } from 'src/app/core/latest-messages.service';
import * as latestMessageActions from './actions';

@Injectable()
export class LatestMessageEffects {
  // Effect for Action 'LoadLatestMessages'
  readonly loadLatestMessagesEffect$ = createEffect(() => this.actions$.pipe(
    ofType(latestMessageActions.loadLatestMessages),
    switchMap(() => this.latestMessageService.getLatestMessages().pipe(
      map(latestMessages => latestMessageActions.loadLatestMessagesSuccess({ latestMessages })),
      catchError(error => of(latestMessageActions.loadLatestMessagesFailure({ error }))),
    )),
  ));

  constructor(
    private readonly latestMessageService: LatestMessagesService,
    private readonly actions$: Actions<latestMessageActions.LatestMessagesUnion>,
  ) {}
}
