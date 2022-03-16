import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as languageActions from './actions';
import * as languageSelectors from './selectors';
import { PartialState } from './state';

@Injectable({ providedIn: 'root' })
export class LanguageFacade {
  readonly languages$ = this.store.select(languageSelectors.selectAvailableLanguages);
  readonly selectedLanguageIso$ = this.store.select(languageSelectors.selectSelectedLanguageIso);

  constructor(private readonly store: Store<PartialState>) {}

  selectLanguage(languageIso: string): void {
    this.store.dispatch(languageActions.selectLanguage({ languageIso }));
  }
}
