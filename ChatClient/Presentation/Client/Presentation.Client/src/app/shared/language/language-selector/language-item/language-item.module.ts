import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageItemComponent } from './language-item.component';



@NgModule({
  declarations: [LanguageItemComponent],
  imports: [CommonModule],
  exports: [LanguageItemComponent]
})
export class LanguageItemModule { }
