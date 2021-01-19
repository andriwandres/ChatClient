import { Injectable } from '@angular/core';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { HttpTransportType, LogLevel } from '@microsoft/signalr';
import { Store } from '@ngrx/store';
import { createSignalRHub, ISignalRHub } from 'ngrx-signalr-core';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class WebSocketFacade {
  constructor(
    private readonly authFacade: AuthFacade,
    private readonly store: Store
  ) {}

  connectWebSocket(): void {
    const hub = {
      hubName: 'Main Hub',
      url: environment.socket.hub,
      options: {
        skipNegotiation: true,
        logger: LogLevel.None,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => {
          return this.authFacade.token$.pipe(
            take(1)
          ).toPromise();
        }
      }
    } as ISignalRHub;

    this.store.dispatch(createSignalRHub(hub));
  }
}
