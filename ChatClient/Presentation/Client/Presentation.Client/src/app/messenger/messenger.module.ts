import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { WebSocketStoreModule } from '../shared/store/messenger';
import { ChatModule } from './chat/chat.module';
import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { SidenavModule } from './sidenav/sidenav.module';

@NgModule({
  declarations: [MessengerComponent],
  imports: [
    CommonModule,
    WebSocketStoreModule,
    MessengerRoutingModule,
    SidenavModule,
    ChatModule,
  ]
})
export class MessengerModule { }
