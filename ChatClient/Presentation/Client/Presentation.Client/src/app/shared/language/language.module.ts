import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LanguageSelectorModule } from './language-selector/language-selector.module';
import { LanguageStoreModule } from './store/language-store.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LanguageStoreModule
  ],
  exports: [LanguageSelectorModule]
})
export class LanguageModule { }
