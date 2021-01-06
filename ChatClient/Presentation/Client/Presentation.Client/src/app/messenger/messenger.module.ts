import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MessengerStoreModule } from '@chat-client/messenger/store';
import { MessengerRoutingModule } from './messenger-routing.module';
import { MessengerComponent } from './messenger.component';
import { SidenavModule } from './sidenav/sidenav.module';

@NgModule({
  declarations: [MessengerComponent],
  imports: [
    CommonModule,
    MessengerStoreModule,
    MessengerRoutingModule,
    SidenavModule
  ]
})
export class MessengerModule { }
