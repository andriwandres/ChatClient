import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignInFormComponent } from './sign-in-form.component';



@NgModule({
  declarations: [SignInFormComponent],
  imports: [
    CommonModule
  ],
  exports: [SignInFormComponent]
})
export class SignInFormModule { }
