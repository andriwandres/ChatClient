import { AbstractControl, ValidationErrors, Validators } from '@angular/forms';

export function emailValidator(control: AbstractControl): ValidationErrors | null {
  if (!control.value) {
    return {
      ...control.errors,
      email: true
    };
  }

  return Validators.email(control);
}
