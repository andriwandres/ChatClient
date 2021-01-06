import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RecipientsComponent } from './recipients.component';
import { RecipientStoreModule } from './store';

@NgModule({
  declarations: [RecipientsComponent],
  imports: [
    CommonModule,
    RecipientStoreModule,
  ],
  exports: [RecipientsComponent]
})
export class RecipientsModule { }
