import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat.component';
import { ChatHeaderModule } from './chat-header/chat-header.module';
import { ChatMessagesModule } from './chat-messages/chat-messages.module';
import { ChatFooterModule } from './chat-footer/chat-footer.module';



@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    ChatHeaderModule,
    ChatMessagesModule,
    ChatFooterModule
  ]
})
export class ChatModule { }
