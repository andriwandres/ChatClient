import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChatsRoutingModule } from './chats-routing.module';
import { ChatsComponent } from './chats.component';
import { SidenavModule } from './sidenav/sidenav.module';
import { ChatModule } from './chat/chat.module';


@NgModule({
  declarations: [ChatsComponent],
  imports: [
    CommonModule,
    ChatsRoutingModule,
    SidenavModule,
    ChatModule
  ]
})
export class ChatsModule { }
