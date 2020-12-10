import { Injectable } from '@angular/core';
import { HttpTransportType } from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { createSignalRHub, ISignalRHub } from 'ngrx-signalr-core';
import { environment } from 'src/environments/environment';

@Injectable()
export class RootStoreFacade {
  constructor(private readonly store: Store) {}

  createHub(): void {
    const hub = {
      hubName: 'ChatClient Hub',
      url: environment.socket.hub,
      options: {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets
      }
    } as ISignalRHub;

    this.store.dispatch(createSignalRHub(hub));
  }
}
