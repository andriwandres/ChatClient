import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatFooterModule } from './chat-footer/chat-footer.module';
import { ChatHeaderModule } from './chat-header/chat-header.module';
import { ChatMessagesModule } from './chat-messages/chat-messages.module';
import { ChatRoutingModule } from './chat-routing.module';
import { ChatComponent } from './chat.component';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    ChatRoutingModule,
    ChatHeaderModule,
    ChatMessagesModule,
    ChatFooterModule
  ]
})
export class ChatModule { }
