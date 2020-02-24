import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { ChatMessagesService } from 'src/app/core/chat-messages.service';
import * as chatMessagesActions from './actions';

@Injectable()
export class ChatMessagesEffects {
  // Effect for Action 'LoadPrivateMessages'
  readonly loadPrivateMessagesEffect$ = this.actions$.pipe(
    ofType(chatMessagesActions.loadPrivateMessages),
    switchMap(action => this.chatMessageService.getPrivateMessages(action.recipientId).pipe(
      map(messages => chatMessagesActions.loadPrivateMessagesSuccess({ messages })),
      tap(() => this.router.navigate([`./user/${action.recipientId}`])),
      catchError(error => of(chatMessagesActions.loadPrivateMessagesFailure({ error }))),
    )),
  );

  // Effect for Action 'LoadGroupMessages'
  readonly loadGroupMessagesEffect$ = this.actions$.pipe(
    ofType(chatMessagesActions.loadGroupMessages),
    switchMap(action => this.chatMessageService.getGroupMessages(action.groupId).pipe(
      map(messages => chatMessagesActions.loadGroupMessagesSuccess({ messages })),
      tap(() => this.router.navigate([`./group/${action.groupId}`])),
      catchError(error => of(chatMessagesActions.loadGroupMessagesFailure({ error }))),
    )),
  );

  constructor(
    private readonly router: Router,
    private readonly chatMessageService: ChatMessagesService,
    private readonly actions$: Actions<chatMessagesActions.ChatMessagesActionUnion>
  ) {}
}
