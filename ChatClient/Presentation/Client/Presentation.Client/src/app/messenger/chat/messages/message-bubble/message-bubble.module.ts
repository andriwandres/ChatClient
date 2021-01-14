import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MessageBubbleComponent } from './message-bubble.component';



@NgModule({
  declarations: [MessageBubbleComponent],
  imports: [
    CommonModule,
    MatIconModule,
  ],
  exports: [MessageBubbleComponent]
})
export class MessageBubbleModule { }
