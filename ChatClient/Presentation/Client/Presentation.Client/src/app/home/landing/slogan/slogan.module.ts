import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { SloganComponent } from './slogan.component';

@NgModule({
  declarations: [SloganComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatIconModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [SloganComponent]
})
export class SloganModule { }
