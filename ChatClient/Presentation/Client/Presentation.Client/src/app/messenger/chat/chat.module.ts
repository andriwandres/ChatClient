import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from './chat.component';
import { ChatHeaderModule } from './header/chat-header.module';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    ChatHeaderModule
  ],
  exports: [ChatComponent]
})
export class ChatModule { }
