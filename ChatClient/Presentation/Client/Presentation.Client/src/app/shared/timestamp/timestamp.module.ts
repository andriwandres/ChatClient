import { NgModule } from '@angular/core';
import { TranslationModule } from '../translation';
import { TimestampPipe } from './timestamp.pipe';

@NgModule({
  declarations: [TimestampPipe],
  imports: [TranslationModule],
  exports: [TimestampPipe],
})
export class TimestampModule {}
