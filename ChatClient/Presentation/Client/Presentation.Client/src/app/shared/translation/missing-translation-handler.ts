import { Injectable } from '@angular/core';
import { MissingTranslationHandler, MissingTranslationHandlerParams } from '@ngx-translate/core';

@Injectable()
export class ChatClientMissingTranslationHandler implements MissingTranslationHandler {
  handle(_: MissingTranslationHandlerParams): string {
    return '';
  }
}
