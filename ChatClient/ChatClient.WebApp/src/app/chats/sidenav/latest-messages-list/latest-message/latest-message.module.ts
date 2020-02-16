import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatTimestampModule } from 'src/app/shared/chat-timestamp/chat-timestamp.module';
import { UserAvatarModule } from 'src/app/shared/user-avatar/user-avatar.module';
import { LatestMessageComponent } from './latest-message.component';

@NgModule({
  declarations: [LatestMessageComponent],
  imports: [
    CommonModule,
    ChatTimestampModule,
    UserAvatarModule
  ],
  exports: [LatestMessageComponent]
})
export class LatestMessageModule { }
