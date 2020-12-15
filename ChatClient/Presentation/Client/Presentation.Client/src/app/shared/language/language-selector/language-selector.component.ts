import { Component, OnInit } from '@angular/core';
import { LanguageFacade } from '@chat-client/shared/language/store';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss']
})
export class LanguageSelectorComponent implements OnInit {
  readonly languages$ = this.languageFacade.languages$;
  readonly selectedLanguage$ = this.languageFacade.selectedLanguage$;
  readonly selectedLanguageId$ = this.languageFacade.selectedLanguageId$;

  constructor(private readonly languageFacade: LanguageFacade) { }

  ngOnInit(): void {
    this.languageFacade.getLanguages();
  }

  selectLanguage(languageId: number): void {
    this.languageFacade.selectLanguage(languageId);
  }
}
