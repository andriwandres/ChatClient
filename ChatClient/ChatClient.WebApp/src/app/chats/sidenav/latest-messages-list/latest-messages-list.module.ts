import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { LatestMessageModule } from './latest-message/latest-message.module';
import { LatestMessagesListComponent } from './latest-messages-list.component';

@NgModule({
  declarations: [LatestMessagesListComponent],
  imports: [
    CommonModule,
    LatestMessageModule
  ],
  exports: [LatestMessagesListComponent]
})
export class LatestMessagesListModule { }
