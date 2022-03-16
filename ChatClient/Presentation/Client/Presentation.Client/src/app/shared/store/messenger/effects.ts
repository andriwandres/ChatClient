import { Injectable } from '@angular/core';
import { ChatMessage } from '@chat-client/core/models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TypedAction } from '@ngrx/store/src/models';
import { mergeMapHubToAction, SIGNALR_HUB_UNSTARTED, startSignalRHub } from 'ngrx-signalr-core';
import { merge, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { MessageActions } from '../messages';

interface ActionEventMapping {
  eventName: string;
  actionFactory: (payload?: any) => TypedAction<string>;
}

const actionMappings: ActionEventMapping[] = [
  {
    eventName: 'ReceiveMessage',
    actionFactory: (payload: { recipientId: number, message: ChatMessage }) => MessageActions.addMessage(payload)
  }
];

@Injectable()
export class WebSocketEffects {
  readonly mapEventsToActions$ = createEffect(() => this.actions$.pipe(
    ofType(SIGNALR_HUB_UNSTARTED),
    mergeMapHubToAction(({ hub }) => {
      const events = actionMappings.map(mapping => hub.on(mapping.eventName).pipe(
        map((payload) => mapping.actionFactory(payload))
      ));

      return merge(
        merge(...events),
        of(startSignalRHub(hub))
      );
    })
  ));

  constructor(private readonly actions$: Actions) {}
}
