import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractControl, FormControl, ValidationErrors } from '@angular/forms';
import { RuleMappings } from '../validation-tooltip/mapping';

@Component({
  selector: 'app-form-field',
  templateUrl: './form-field.component.html',
  styleUrls: ['./form-field.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormFieldComponent {
  @Input() control!: FormControl;
  @Input() placeholder!: string;

  @Input() type = 'text';
  @Input() hint?: string;
  @Input() prefixIcon?: string;
  @Input() suffixIcon?: string;
  @Input() ruleMappings?: RuleMappings;

  @Output() suffixClick = new EventEmitter();

  onSuffixClick(event: MouseEvent): void {
    event.stopPropagation();

    this.suffixClick.emit();
  }
}
