import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessagesComponent } from './messages.component';



@NgModule({
  declarations: [MessagesComponent],
  imports: [
    CommonModule
  ],
  exports: [MessagesComponent]
})
export class MessagesModule { }
