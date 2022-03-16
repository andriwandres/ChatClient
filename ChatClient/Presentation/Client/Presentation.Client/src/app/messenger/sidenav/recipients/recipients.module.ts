import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RecipientStoreModule } from 'src/app/shared/store/recipients';
import { RecipientModule } from './recipient/recipient.module';
import { RecipientsComponent } from './recipients.component';

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
