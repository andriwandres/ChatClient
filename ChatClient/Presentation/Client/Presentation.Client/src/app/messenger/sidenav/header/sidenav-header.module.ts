import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidenavHeaderComponent } from './sidenav-header.component';

@NgModule({
  declarations: [SidenavHeaderComponent],
  imports: [
    CommonModule
  ],
  exports: [SidenavHeaderComponent]
})
export class SidenavHeaderModule { }
