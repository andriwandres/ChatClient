import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidationTooltipDirective } from './validation-tooltip.directive';
import { ValidationTooltipComponent } from './validation-tooltip.component';

@NgModule({
  declarations: [ValidationTooltipDirective, ValidationTooltipComponent],
  imports: [CommonModule],
  exports: [ValidationTooltipDirective]
})
export class ValidationTooltipModule { }
