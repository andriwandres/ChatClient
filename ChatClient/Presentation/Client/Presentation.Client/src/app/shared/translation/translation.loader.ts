import { TranslationDictionary } from '@chat-client/core/models';
import { TranslationService } from '@chat-client/core/services';
import { TranslateLoader } from '@ngx-translate/core';
import { Observable } from 'rxjs';

export class TranslationLoader implements TranslateLoader {
  constructor(private readonly translationFacade: TranslationService) {}

  getTranslation(lang: string): Observable<TranslationDictionary> {
    return this.translationFacade.getTranslations(+lang);
  }
}
