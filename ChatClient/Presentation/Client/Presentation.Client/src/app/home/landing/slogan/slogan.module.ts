import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SloganComponent } from './slogan.component';



@NgModule({
  declarations: [SloganComponent],
  imports: [
    CommonModule
  ],
  exports: [SloganComponent]
})
export class SloganModule { }
