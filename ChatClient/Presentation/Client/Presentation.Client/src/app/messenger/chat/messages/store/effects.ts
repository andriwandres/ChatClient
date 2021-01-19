import { Injectable } from '@angular/core';
import { ApiError } from '@chat-client/core/models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { MessageService } from 'src/app/core/services/message.service';
import * as messagesActions from './actions';

@Injectable()
export class MessageEffects {
  readonly loadMessages$ = createEffect(() =>
    this.actions$.pipe(
      ofType(messagesActions.loadMessages),
      switchMap(({ recipientId, before }) =>
        this.messagesService.getMessages(recipientId, {
          before,
          limit: 50,
        }).pipe(
          map((messages) => {
            // Load messages before a given date
            if (before) {
              return messagesActions.loadPreviousMessagesSuccess({
                result: [recipientId, messages]
              });
            }

            // Just load messages and replace state completely
            return messagesActions.loadMessagesSuccess({
              result: [recipientId, messages],
            });
          }),
          catchError((error: ApiError) =>
            of(messagesActions.loadMessagesFailure({ error }))
          )
        )
      )
    )
  );

  constructor(
    private readonly actions$: Actions,
    private readonly messagesService: MessageService
  ) {}
}
