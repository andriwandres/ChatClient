import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { ReactiveComponentModule } from '@ngrx/component';
import { LanguageItemModule } from './language-item/language-item.module';
import { LanguageSelectorComponent } from './language-selector.component';

@NgModule({
  declarations: [LanguageSelectorComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatMenuModule,
    LanguageItemModule,
    ReactiveComponentModule,
  ],
  exports: [LanguageSelectorComponent]
})
export class LanguageSelectorModule { }
