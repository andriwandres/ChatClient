import { KeyValue } from '@angular/common';
import { Component, Input } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { Rule, RuleMappings } from './mapping';

@Component({
  selector: 'app-validation-tooltip',
  templateUrl: './validation-tooltip.component.html',
  styleUrls: ['./validation-tooltip.component.scss']
})
export class ValidationTooltipComponent {
  @Input() errors!: ValidationErrors;
  @Input() ruleMappings!: RuleMappings;

  trackByKey(index: number, item: KeyValue<string, Rule>): string {
    return item.key;
  }
}
