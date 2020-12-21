import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';
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
    MatIconModule,
    TranslateModule.forChild({ extend: true })
  ],
  exports: [ValidationTooltipDirective]
})
export class ValidationTooltipModule { }
