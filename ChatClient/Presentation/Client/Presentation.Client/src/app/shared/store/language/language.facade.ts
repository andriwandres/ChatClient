import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as languageActions from './language.actions';
import * as languageSelectors from './language.selectors';
import { PartialState } from './language.state';

@Injectable({ providedIn: 'root' })
export class LanguageFacade {
  readonly languages$ = this.store.select(languageSelectors.selectAvailableLanguages);
  readonly selectedLanguageIso$ = this.store.select(languageSelectors.selectSelectedLanguageIso);

  constructor(private readonly store: Store<PartialState>) {}

  selectLanguage(languageIso: string): void {
    this.store.dispatch(languageActions.selectLanguage({ languageIso }));
  }
}
