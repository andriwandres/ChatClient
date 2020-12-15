import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageItemComponent } from './language-item.component';
import { MatMenuModule } from '@angular/material/menu';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [LanguageItemComponent],
  imports: [
    CommonModule,
    MatMenuModule,
    TranslateModule.forChild({ extend: true,  })
  ],
  exports: [LanguageItemComponent]
})
export class LanguageItemModule { }
