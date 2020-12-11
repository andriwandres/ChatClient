import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MessengerStoreModule } from '@chat-client/messenger/store';
import { MessengerComponent } from './messenger.component';

@NgModule({
  declarations: [MessengerComponent],
  imports: [
    CommonModule,
    MessengerStoreModule
  ]
})
export class MessengerModule { }
