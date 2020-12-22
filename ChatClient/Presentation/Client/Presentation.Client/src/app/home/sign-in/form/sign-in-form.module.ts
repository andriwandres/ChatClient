import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FormBuilderModule } from 'src/app/shared/form-builder';
import { SignInFormComponent } from './sign-in-form.component';

@NgModule({
  declarations: [SignInFormComponent],
  imports: [
    CommonModule,
    RouterModule,
    MatInputModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    FormBuilderModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [SignInFormComponent]
})
export class SignInFormModule { }
