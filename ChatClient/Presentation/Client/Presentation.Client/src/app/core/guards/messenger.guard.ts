import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlSegment } from '@angular/router';
import { AuthFacade } from '@chat-client/auth/store';
import { Observable } from 'rxjs';
import { map, skipWhile, tap, withLatestFrom } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MessengerGuard implements CanLoad {
  constructor(
    private readonly router: Router,
    private readonly authFacade: AuthFacade
  ) {}
  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    return this.authFacade.authenticationAttempted$.pipe(
      skipWhile(attempted => !attempted),
      withLatestFrom(this.authFacade.authenticationSuccessful$),
      map(([attempted, successful]) => attempted && successful),
      tap(result => {
        if (result === false) {
          this.router.navigate(['home']);
        }
      })
    );
  }
}
