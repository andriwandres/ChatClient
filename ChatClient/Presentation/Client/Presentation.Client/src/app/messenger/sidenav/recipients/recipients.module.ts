import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RecipientModule } from './recipient/recipient.module';
import { RecipientsComponent } from './recipients.component';
import { RecipientStoreModule } from './store';

@NgModule({
  declarations: [RecipientsComponent],
  imports: [
    CommonModule,
    RecipientStoreModule,
    RecipientModule
  ],
  exports: [RecipientsComponent]
})
export class RecipientsModule { }
