import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, exhaustMap, map, switchMap } from 'rxjs/operators';
import { ChatMessagesService } from 'src/app/core/chat-messages.service';
import * as chatMessagesActions from './actions';

@Injectable()
export class ChatMessagesEffects {
  // Effect for Action 'LoadPrivateMessages'
  readonly loadPrivateMessagesEffect$ = createEffect(() => this.actions$.pipe(
    ofType(chatMessagesActions.loadPrivateMessages),
    switchMap(action => this.chatMessageService.getPrivateMessages(action.recipientId).pipe(
      map(messages => chatMessagesActions.loadPrivateMessagesSuccess({ messages })),
      catchError(error => of(chatMessagesActions.loadPrivateMessagesFailure({ error }))),
    )),
  ));

  // Effect for Action 'LoadGroupMessages'
  readonly loadGroupMessagesEffect$ = createEffect(() => this.actions$.pipe(
    ofType(chatMessagesActions.loadGroupMessages),
    switchMap(action => this.chatMessageService.getGroupMessages(action.groupId).pipe(
      map(messages => chatMessagesActions.loadGroupMessagesSuccess({ messages })),
      catchError(error => of(chatMessagesActions.loadGroupMessagesFailure({ error }))),
    )),
  ));

  // Effect for Action 'AddPrivateMessage'
  readonly addPrivateMessage$ = createEffect(() => this.actions$.pipe(
    ofType(chatMessagesActions.addPrivateMessage),
    exhaustMap(action => this.chatMessageService.addPrivateMessage(action.message).pipe(
      map(message => chatMessagesActions.addPrivateMessageSuccess({ message })),
      catchError(error => of(chatMessagesActions.addPrivateMessageFailure({ error })))
    ))
  ));

  // Effect for Action 'AddPrivateMessage'
  readonly addGroupMessage$ = createEffect(() => this.actions$.pipe(
    ofType(chatMessagesActions.addGroupMessage),
    exhaustMap(action => this.chatMessageService.addGroupMessage(action.message).pipe(
      map(message => chatMessagesActions.addGroupMessageSuccess({ message })),
      catchError(error => of(chatMessagesActions.addGroupMessageFailure({ error })))
    ))
  ));

  constructor(
    private readonly chatMessageService: ChatMessagesService,
    private readonly actions$: Actions<chatMessagesActions.ChatMessagesActionUnion>
  ) {}
}
