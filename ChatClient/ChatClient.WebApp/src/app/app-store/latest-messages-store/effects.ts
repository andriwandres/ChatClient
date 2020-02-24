import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { LatestMessagesService } from 'src/app/core/latest-messages.service';
import * as latestMessageActions from './actions';
import { Router } from '@angular/router';

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

  // Effect for Action 'SetActiveMessageEffect'
  readonly setActiveMessageEffect$ = createEffect(() => this.actions$.pipe(
    ofType(latestMessageActions.selectActiveMessage),
    tap(({ message }) => {
      const isGroupChat = !!message.groupRecipient;

      const targetId = isGroupChat
        ? message.groupRecipient.groupId
        : message.userRecipient.userId;

      const urlSegment = isGroupChat
        ? `chats/group/${targetId}`
        : `chats/user/${targetId}`;

      // Navigate to either group/private chat
      this.router.navigate([urlSegment]);
    })
  ), { dispatch: false });

  constructor(
    private readonly router: Router,
    private readonly latestMessageService: LatestMessagesService,
    private readonly actions$: Actions<latestMessageActions.LatestMessagesUnion>,
  ) {}
}
