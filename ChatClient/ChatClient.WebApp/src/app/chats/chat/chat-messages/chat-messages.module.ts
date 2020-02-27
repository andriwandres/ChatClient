import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatMessagesComponent } from './chat-messages.component';
import { ChatBubbleModule } from './chat-bubble/chat-bubble.module';

@NgModule({
  declarations: [ChatMessagesComponent],
  imports: [
    CommonModule,
    ChatBubbleModule
  ],
  exports: [ChatMessagesComponent]
})
export class ChatMessagesModule { }
