import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessagesComponent } from './messages.component';
import { MessageBubbleModule } from './message-bubble/message-bubble.module';
import { MessagesStoreModule } from 'src/app/shared/store/messages';



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
