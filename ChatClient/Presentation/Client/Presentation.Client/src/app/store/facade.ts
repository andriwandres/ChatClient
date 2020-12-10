import { Injectable } from '@angular/core';
import { AuthFacade } from '@chat-client/auth/store';
import { HttpTransportType } from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { createSignalRHub, ISignalRHub } from 'ngrx-signalr-core';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PartialState } from '../auth/store/state';

@Injectable()
export class RootStoreFacade {
  constructor(
    private readonly authFacade: AuthFacade,
    private readonly store: Store<PartialState>
  ) {}

  createHub(): void {
    const hub = {
      hubName: 'ChatClient Hub',
      url: environment.socket.hub,
      options: {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        // accessTokenFactory: () => {
        //   return this.authFacade.token$.pipe(
        //     take(1)
        //   ).toPromise();
        // }
      }
    } as ISignalRHub;

    this.store.dispatch(createSignalRHub(hub));
  }
}
