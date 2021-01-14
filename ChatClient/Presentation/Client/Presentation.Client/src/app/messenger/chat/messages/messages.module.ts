import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessagesComponent } from './messages.component';
import { MessagesStoreModule } from './store/messages-store.module';
import { MessageBubbleModule } from './message-bubble/message-bubble.module';



@NgModule({
  declarations: [MessagesComponent],
  imports: [
    CommonModule,
    MessagesStoreModule,
    MessageBubbleModule
  ],
  exports: [MessagesComponent]
})
export class MessagesModule { }
