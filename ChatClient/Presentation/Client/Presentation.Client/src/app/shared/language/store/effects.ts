import { Injectable } from '@angular/core';
import { ApiError } from '@chat-client/core/models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { TranslateService } from '@ngx-translate/core';
import { of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { LanguageService } from 'src/app/core/services/language.service';
import * as languageActions from './actions';

@Injectable({ providedIn: 'root' })
export class LanguageEffects {
  readonly getLanguages$ = createEffect(() => this.actions$.pipe(
    ofType(languageActions.getLanguages),
    switchMap(() => this.languageService.getLanguages().pipe(
      tap(languages => {
        if (!localStorage.getItem('languageId')) {
          const languageId = languages[0].languageId.toString();

          this.translateService.use(languageId);
          localStorage.setItem('languageId', languageId);
        } else {
          this.translateService.use(localStorage.getItem('languageId') || '');
        }
      }),
      map(languages => languageActions.getLanguagesSuccess({ languages })),
      catchError((error: ApiError) => of(languageActions.getLanguagesFailure({ error })))
    ))
  ));

  readonly selectLanguage$ = createEffect(() => this.actions$.pipe(
    ofType(languageActions.selectLanguage),
    tap(({ languageId }) => {
      localStorage.setItem('languageId', languageId.toString());
      this.translateService.use(`${languageId}`);
    })
  ), {
    dispatch: false
  });

  constructor(
    private readonly actions$: Actions,
    private readonly languageService: LanguageService,
    private readonly translateService: TranslateService,
  ) {}
}
