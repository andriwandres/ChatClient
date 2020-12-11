import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MessengerStoreModule } from '@chat-client/messenger/store';
import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';

@NgModule({
  declarations: [MessengerComponent],
  imports: [
    CommonModule,
    MessengerStoreModule,
    MessengerRoutingModule
  ]
})
export class MessengerModule { }
