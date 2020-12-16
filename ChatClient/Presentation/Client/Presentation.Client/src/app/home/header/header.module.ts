import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { LanguageModule } from '@chat-client/shared/language';
import { TranslateModule } from '@ngx-translate/core';
import { HeaderComponent } from './header.component';

@NgModule({
  imports: [
    CommonModule,
    LanguageModule,
    MatIconModule,
    RouterModule,
    TranslateModule.forChild({ extend: true })
  ],
  declarations: [HeaderComponent],
  exports: [HeaderComponent]
})
export class HeaderModule { }
