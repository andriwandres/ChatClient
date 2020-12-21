import { AbstractControl, ValidationErrors, Validators } from '@angular/forms';

const EMAIL_REGEX = /^(?=.{1,254}$)(?=.{1,64}@)[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

export function emailValidator(control: AbstractControl): ValidationErrors | null {
  if (!control.value) {
    return { email: true };
  }

  const isEmail = EMAIL_REGEX.test(control.value);

  return isEmail ? null : { email: true };
}
