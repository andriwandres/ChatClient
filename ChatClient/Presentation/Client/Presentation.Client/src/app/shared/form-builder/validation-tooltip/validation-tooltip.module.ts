import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { RuleOrderPipe } from './rule-order.pipe';
import { ValidationTooltipComponent } from './validation-tooltip.component';
import { ValidationTooltipDirective } from './validation-tooltip.directive';

@NgModule({
  declarations: [
    ValidationTooltipDirective,
    ValidationTooltipComponent,
    RuleOrderPipe
  ],
  imports: [
    CommonModule,
    MatIconModule
  ],
  exports: [ValidationTooltipDirective]
})
export class ValidationTooltipModule { }
