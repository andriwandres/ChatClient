import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatHeaderComponent } from './chat-header.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [ChatHeaderComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [ChatHeaderComponent]
})
export class ChatHeaderModule { }
