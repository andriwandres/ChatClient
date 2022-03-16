import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment } from '@angular/router';
import { AuthFacade } from 'src/app/shared/store/auth';

@Injectable({ providedIn: 'root' })
export class HomeGuard implements CanLoad, CanActivate {
  constructor(private readonly authFacade: AuthFacade) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean  {
    return true;
  }

  canLoad(route: Route, segments: UrlSegment[]): boolean {
    return true;
  }
}
