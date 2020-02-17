import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { DisplayImageComponent } from 'src/app/shared/display-image/display-image.component';
import { ChatHeaderComponent } from './chat-header.component';

@NgModule({
  declarations: [ChatHeaderComponent],
  imports: [
    CommonModule,
    MatButtonModule,
    MatTooltipModule,
    MatIconModule,
    DisplayImageComponent
  ],
  exports: [ChatHeaderComponent]
})
export class ChatHeaderModule { }
