import { Component } from '@angular/core';
import { LanguageFacade } from '@chat-client/shared/language/store';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss']
})
export class LanguageSelectorComponent {
  readonly languages$ = this.languageFacade.languages$;
  readonly selectedLanguageIso$ = this.languageFacade.selectedLanguageIso$;

  constructor(private readonly languageFacade: LanguageFacade) { }

  selectLanguage(languageIso: string): void {
    this.languageFacade.selectLanguage(languageIso);
  }
}
