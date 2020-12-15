import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment } from '@angular/router';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { Observable } from 'rxjs';
import { map, skipWhile, tap, withLatestFrom } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MessengerGuard implements CanLoad, CanActivate {
  constructor(
    private readonly router: Router,
    private readonly authFacade: AuthFacade
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.isAuthenticated();
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    return this.isAuthenticated();
  }

  private isAuthenticated(): Observable<boolean> {
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
