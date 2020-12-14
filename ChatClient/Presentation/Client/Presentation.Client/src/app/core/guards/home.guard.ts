import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment } from '@angular/router';
import { AuthFacade } from '@chat-client/auth/store';
import { Observable } from 'rxjs';
import { skipWhile, tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class HomeGuard implements CanLoad {
  constructor(private readonly authFacade: AuthFacade) {}

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    return this.authFacade.authenticationAttempted$.pipe(
      skipWhile(result => !result),
    );
  }
}
