import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LanguageStoreModule } from '../store/language/language-store.module';
import { LanguageSelectorModule } from './language-selector/language-selector.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LanguageStoreModule
  ],
  exports: [LanguageSelectorModule]
})
export class LanguageModule { }
