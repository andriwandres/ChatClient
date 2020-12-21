import { Component, Input } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { RuleMappings } from './mapping';

@Component({
  selector: 'app-validation-tooltip',
  templateUrl: './validation-tooltip.component.html',
  styleUrls: ['./validation-tooltip.component.scss']
})
export class ValidationTooltipComponent {
  @Input() ruleMappings!: RuleMappings;
  @Input() errors!: ValidationErrors;
}
