import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { mergeMap, take } from 'rxjs/operators';
import { AppStoreState } from 'src/app/app-store';
import { AuthStoreSelectors } from 'src/app/app-store/auth-store';

@Injectable({ providedIn: 'root' })
export class AuthInterceptor implements HttpInterceptor {
  constructor(private readonly store$: Store<AppStoreState.State>) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token$ = this.store$.select(AuthStoreSelectors.selectToken);

    return token$.pipe(
      take(1),
      mergeMap(token => {
        if (token) {
          request = request.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          });
        }

        return next.handle(request);
      }),
    );
  }
}
