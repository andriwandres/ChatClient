import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatBubbleComponent } from './chat-bubble.component';

@NgModule({
  declarations: [ChatBubbleComponent],
  imports: [
    CommonModule
  ],
  exports: [ChatBubbleComponent]
})
export class ChatBubbleModule { }
