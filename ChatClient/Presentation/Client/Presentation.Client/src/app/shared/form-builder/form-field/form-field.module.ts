import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { RuleOrderPipe } from './error-order.pipe';
import { FormFieldComponent } from './form-field.component';

@NgModule({
  declarations: [FormFieldComponent, RuleOrderPipe],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    RouterModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [FormFieldComponent]
})
export class FormFieldModule { }
