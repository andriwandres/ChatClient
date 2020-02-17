import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatTimestampModule } from 'src/app/shared/chat-timestamp/chat-timestamp.module';
import { DisplayImageModule } from 'src/app/shared/display-image/display-image.module';
import { LatestMessageComponent } from './latest-message.component';

@NgModule({
  declarations: [LatestMessageComponent],
  imports: [
    CommonModule,
    ChatTimestampModule,
    DisplayImageModule
  ],
  exports: [LatestMessageComponent]
})
export class LatestMessageModule { }
