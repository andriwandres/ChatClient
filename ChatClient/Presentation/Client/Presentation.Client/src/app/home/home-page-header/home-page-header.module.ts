import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { LanguageModule } from '@chat-client/shared/language';
import { HomePageHeaderComponent } from './home-page-header.component';

@NgModule({
  imports: [
    CommonModule,
    LanguageModule,
    MatIconModule
  ],
  declarations: [HomePageHeaderComponent],
  exports: [HomePageHeaderComponent]
})
export class HomePageHeaderModule { }
