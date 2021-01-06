import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { SidenavHeaderModule } from './header/sidenav-header.module';
import { RecipientsModule } from './recipients/recipients.module';
import { SidenavComponent } from './sidenav.component';

@NgModule({
  declarations: [SidenavComponent],
  imports: [
    CommonModule,
    MatSidenavModule,
    SidenavHeaderModule,
    RecipientsModule,
  ],
  exports: [SidenavComponent],
})
export class SidenavModule {}
