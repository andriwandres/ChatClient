import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateAccountRoutingModule } from './create-account-routing.module';
import { CreateAccountComponent } from './create-account.component';
import { CreateAccountFormModule } from './form/create-account-form.module';
import { CreateAccountIllustrationModule } from './illustration/create-account-illustration.module';


@NgModule({
  declarations: [CreateAccountComponent],
  imports: [
    CommonModule,
    CreateAccountRoutingModule,
    CreateAccountFormModule,
    CreateAccountIllustrationModule
  ]
})
export class CreateAccountModule { }
