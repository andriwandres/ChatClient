import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FormBuilderModule } from 'src/app/shared/form-builder/form-builder.module';
import { CreateAccountFormComponent } from './create-account-form.component';

@NgModule({
  declarations: [CreateAccountFormComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    FormBuilderModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [CreateAccountFormComponent]
})
export class CreateAccountFormModule { }
