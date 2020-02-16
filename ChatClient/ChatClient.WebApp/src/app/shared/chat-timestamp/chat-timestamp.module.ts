import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ChatTimestampPipe } from './chat-timestamp.pipe';

@NgModule({
  declarations: [ChatTimestampPipe],
  imports: [CommonModule],
  exports: [ChatTimestampPipe]
})
export class ChatTimestampModule { }
