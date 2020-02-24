import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LatestMessageModule } from './latest-message/latest-message.module';
import { LatestMessagesListComponent } from './latest-messages-list.component';

@NgModule({
  declarations: [LatestMessagesListComponent],
  imports: [
    CommonModule,
    LatestMessageModule,
    MatProgressSpinnerModule
  ],
  exports: [LatestMessagesListComponent]
})
export class LatestMessagesListModule { }
