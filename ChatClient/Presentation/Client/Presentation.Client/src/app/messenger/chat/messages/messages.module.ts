import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MessagesComponent } from './messages.component';
import { MessagesStoreModule } from './store/messages-store.module';



@NgModule({
  declarations: [MessagesComponent],
  imports: [
    CommonModule,
    MessagesStoreModule
  ],
  exports: [MessagesComponent]
})
export class MessagesModule { }
