import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { LanguageModule } from '@chat-client/shared/language';
import { TranslateModule } from '@ngx-translate/core';
import { HomePageHeaderComponent } from './home-page-header.component';

@NgModule({
  imports: [
    CommonModule,
    LanguageModule,
    MatIconModule,
    RouterModule,
    TranslateModule.forChild({ extend: true })
  ],
  declarations: [HomePageHeaderComponent],
  exports: [HomePageHeaderComponent]
})
export class HomePageHeaderModule { }
