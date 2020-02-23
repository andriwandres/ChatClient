import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LatestMessagesListModule } from './latest-messages-list/latest-messages-list.module';
import { SidenavHeaderModule } from './sidenav-header/sidenav-header.module';
import { SidenavComponent } from './sidenav.component';

@NgModule({
  declarations: [SidenavComponent],
  imports: [
    CommonModule,
    SidenavHeaderModule,
    LatestMessagesListModule,
  ],
  exports: [SidenavComponent]
})
export class SidenavModule { }
