import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ChatActionsComponent } from './chat-actions.component';



@NgModule({
  declarations: [ChatActionsComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [ChatActionsComponent]
})
export class ChatActionsModule { }
