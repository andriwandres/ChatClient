import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FormBuilderModule } from 'src/app/shared/form-builder/form-builder.module';
import { CreateAccountFormComponent } from './create-account-form.component';

@NgModule({
  declarations: [CreateAccountFormComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormBuilderModule,
  ],
  exports: [CreateAccountFormComponent]
})
export class CreateAccountFormModule { }
