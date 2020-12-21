import { ValidatorFn, Validators } from '@angular/forms';

export function minLengthValidator(minLength: number): ValidatorFn  {
  return ((control) => {
    if (!control.value) {
      return { minlength: true };
    }

    return Validators.minLength(minLength)(control);
  });
}
