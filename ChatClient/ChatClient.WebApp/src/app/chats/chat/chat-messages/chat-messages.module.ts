import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatMessagesComponent } from './chat-messages.component';



@NgModule({
  declarations: [ChatMessagesComponent],
  imports: [
    CommonModule
  ],
  exports: [ChatMessagesComponent]
})
export class ChatMessagesModule { }
