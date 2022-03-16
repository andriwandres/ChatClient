import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment } from '@angular/router';
import { Observable } from 'rxjs';
import { map, skipWhile, switchMapTo, tap } from 'rxjs/operators';
import { AuthFacade } from 'src/app/shared/store/auth';

@Injectable({ providedIn: 'root' })
export class SignInGuard implements CanLoad, CanActivate {
  constructor(
    private readonly router: Router,
    private readonly authFacade: AuthFacade
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.isAnonymous();
  }

  canLoad(route: Route, segments: UrlSegment[]): Observable<boolean> {
    return this.isAnonymous();
  }

  private isAnonymous(): Observable<boolean> {
    return this.authFacade.authenticationAttempted$.pipe(
      skipWhile(attempted => !attempted),
      switchMapTo(this.authFacade.authenticationSuccessful$),
      map(successful => !successful),
      tap(result => {
        if (result === false) {
          this.router.navigateByUrl('');
        }
      })
    );
  }
}
