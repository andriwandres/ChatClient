import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TranslateService } from '@ngx-translate/core';
import { tap } from 'rxjs/operators';
import * as languageActions from './language.actions';

@Injectable({ providedIn: 'root' })
export class LanguageEffects {
  readonly selectLanguage$ = createEffect(() => this.actions$.pipe(
    ofType(languageActions.selectLanguage),
    tap(({ languageIso }) => {
      localStorage.setItem('languageIso', languageIso);
      this.translateService.use(languageIso);
    })
  ), {
    dispatch: false
  });

  constructor(
    private readonly actions$: Actions,
    private readonly translateService: TranslateService,
  ) {}
}
