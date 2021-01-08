import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { TimestampPipe } from './timestamp.pipe';

@NgModule({
  declarations: [TimestampPipe],
  imports: [
    TranslateModule.forChild({ extend: true })
  ],
  exports: [TimestampPipe],
})
export class TimestampModule {}
