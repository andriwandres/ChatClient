import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatActionsModule } from './actions/chat-actions.module';
import { ChatComponent } from './chat.component';
import { ChatHeaderModule } from './header/chat-header.module';
import { MessagesModule } from './messages/messages.module';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    ChatHeaderModule,
    MessagesModule,
    ChatActionsModule
  ],
  exports: [ChatComponent]
})
export class ChatModule { }
