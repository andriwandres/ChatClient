import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidenavHeaderComponent } from './sidenav-header.component';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [SidenavHeaderComponent],
  imports: [
    CommonModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [SidenavHeaderComponent]
})
export class SidenavHeaderModule { }
