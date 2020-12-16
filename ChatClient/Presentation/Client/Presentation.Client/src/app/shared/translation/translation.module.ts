import { NgModule } from '@angular/core';
import { TranslationService } from '@chat-client/core/services';
import { MissingTranslationHandler, TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { ChatClientTranslationLoader } from './loader';
import { ChatClientMissingTranslationHandler } from './missing-translation-handler';

@NgModule({
  imports: [
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: ChatClientTranslationLoader,
        deps: [TranslationService]
      },
      missingTranslationHandler: {
        provide: MissingTranslationHandler,
        useClass: ChatClientMissingTranslationHandler,
        deps: []
      }
    })
  ],
})
export class TranslationModule {}
