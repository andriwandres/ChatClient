import { Injectable } from '@angular/core';
import { ApiError } from '@chat-client/core/models';
import { Actions, createEffect, ofType } from '@ngrx/effects';
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
          localStorage.setItem('languageId', languages[0].languageId.toString());
        }
      }),
      map(languages => languageActions.getLanguagesSuccess({ languages })),
      catchError((error: ApiError) => of(languageActions.getLanguagesFailure({ error })))
    ))
  ));

  readonly selectLanguage$ = createEffect(() => this.actions$.pipe(
    ofType(languageActions.selectLanguage),
    tap(({ languageId }) => localStorage.setItem('languageId', languageId.toString()))
  ), {
    dispatch: false
  });

  constructor(
    private readonly actions$: Actions,
    private readonly languageService: LanguageService,
  ) {}
}
