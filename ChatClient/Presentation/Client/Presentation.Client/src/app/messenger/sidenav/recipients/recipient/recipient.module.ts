import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
import { TimestampModule } from 'src/app/shared/timestamp/timestamp.module';
import { RecipientComponent } from './recipient.component';
import { StripHtmlPipe } from './strip-html.pipe';

@NgModule({
  declarations: [RecipientComponent, StripHtmlPipe],
  imports: [
    CommonModule,
    TimestampModule,
    MatIconModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [RecipientComponent]
})
export class RecipientModule { }
