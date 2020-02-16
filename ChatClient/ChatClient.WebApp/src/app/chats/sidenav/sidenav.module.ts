import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SidenavComponent } from './sidenav.component';



@NgModule({
  declarations: [SidenavComponent],
  imports: [
    CommonModule
  ],
  exports: [SidenavComponent]
})
export class SidenavModule { }
