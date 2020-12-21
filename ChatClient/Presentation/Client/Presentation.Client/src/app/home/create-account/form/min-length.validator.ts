import { ValidatorFn } from '@angular/forms';

export function minLengthValidator(minLength: number): ValidatorFn  {
  return ((control) => {
    const value = control.value as string;

    return !value || value.length < minLength
      ? { minlength: true }
      : null;
  });
}
