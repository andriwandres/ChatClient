import { Injectable } from '@angular/core';
import { AuthActions, AuthFacade } from '@chat-client/auth/store';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { ActionCreator } from '@ngrx/store';
import { mergeMapHubToAction, SIGNALR_HUB_UNSTARTED, startSignalRHub } from 'ngrx-signalr-core';
import { merge, of } from 'rxjs';
import { map } from 'rxjs/operators';

interface ActionEventMapping {
  eventName: string;
  action: ActionCreator;
}

const actionMappings: ActionEventMapping[] = [
  {
    eventName: 'receiveMessage',
    action: AuthActions.resetAvailabilityChecks
  }
];

@Injectable()
export class RootStoreEffects {
  readonly mapActions$ = createEffect(() => this.actions$.pipe(
    ofType(SIGNALR_HUB_UNSTARTED),
    mergeMapHubToAction(({ hub }) => {
      const events = actionMappings.map(mapping => hub.on(mapping.eventName).pipe(
        map((props) => mapping.action))
      );

      return merge(
        merge(...events),
        of(startSignalRHub(hub))
      );
    })
  ));

  constructor(
    private readonly actions$: Actions,
  ) {}
}
