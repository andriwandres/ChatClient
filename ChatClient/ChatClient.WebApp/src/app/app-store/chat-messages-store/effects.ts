import { Injectable } from '@angular/core';
import { ChatMessagesService } from 'src/app/core/chat-messages.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as chatMessagesActions from './actions';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable()
export class ChatMessagesEffects {
  readonly loadPrivateMessagesEffect$ = this.actions$.pipe(
    ofType(chatMessagesActions.loadPrivateMessages),
    switchMap(action => this.chatMessageService.getPrivateMessages(action.recipientId).pipe(
      map(messages => chatMessagesActions.loadPrivateMessagesSuccess({ messages })),
      catchError(error => of(chatMessagesActions.loadPrivateMessagesFailure({ error }))),
    )),
  );

  readonly loadGroupMessagesEffect$ = this.actions$.pipe(
    ofType(chatMessagesActions.loadGroupMessages),
    switchMap(action => this.chatMessageService.getGroupMessages(action.groupId).pipe(
      map(messages => chatMessagesActions.loadGroupMessagesSuccess({ messages })),
      catchError(error => of(chatMessagesActions.loadGroupMessagesFailure({ error }))),
    )),
  );

  constructor(
    private readonly chatMessageService: ChatMessagesService,
    private readonly actions$: Actions<chatMessagesActions.ChatMessagesActionUnion>
  ) {}
}
