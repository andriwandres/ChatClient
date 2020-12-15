import { NgModule } from '@angular/core';
import { TranslationService } from '@chat-client/core/services';
import { MissingTranslationHandler, TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslationLoader } from './translation.loader';

@NgModule({
  imports: [
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: TranslationLoader,
        deps: [TranslationService]
      },
    })
  ],
  providers: [
    {
      provide: MissingTranslationHandler,
      useValue: () => ''
    }
  ],
})
export class TranslationModule {}
