import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as languageActions from './actions';
import * as languageSelectors from './selectors';
import { PartialState } from './state';

@Injectable({ providedIn: 'root' })
export class LanguageFacade {
  readonly languages$ = this.store.select(languageSelectors.selectAll);
  readonly selectedLanguage$ = this.store.select(languageSelectors.selectSelectedLanguage);
  readonly selectedLanguageId$ = this.store.select(languageSelectors.selectSelectedLanguageId);

  constructor(private readonly store: Store<PartialState>) {}

  getLanguages(): void {
    this.store.dispatch(languageActions.getLanguages());
  }

  selectLanguage(languageId: number): void {
    this.store.dispatch(languageActions.selectLanguage({ languageId }));
  }
}
