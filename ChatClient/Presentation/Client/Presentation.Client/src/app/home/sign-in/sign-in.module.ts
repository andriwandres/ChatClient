import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SignInFormModule } from './form/sign-in-form.module';
import { SignInIllustrationModule } from './illustration/sign-in-illustration.module';
import { SignInRoutingModule } from './sign-in-routing.module';
import { SignInComponent } from './sign-in.component';

@NgModule({
  declarations: [SignInComponent],
  imports: [
    CommonModule,
    SignInRoutingModule,
    SignInIllustrationModule,
    SignInFormModule,
  ]
})
export class SignInModule { }
