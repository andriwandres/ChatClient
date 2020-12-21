import { AbstractControl, FormGroup, ValidatorFn } from '@angular/forms';

/**
 * Validates two controls in a form group to have matching values
 *
 * @param controlName Name of control name
 * @param  matchingControlName Name of matching control name
 *
 * @returns Validator function used for validation
 */
export function mustMatch(controlName: string, matchingControlName: string): ValidatorFn  {
  return ((group: AbstractControl) => {
    const formGroup = group as FormGroup;

    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    if (control.value !== matchingControl.value) {
      const remainingErrors = matchingControl.errors || [];

      matchingControl.setErrors({
        ...remainingErrors,
        misMatch: true
      });
    } else if (matchingControl.errors) {
      const count = Object.keys(matchingControl.errors).length;
      const exists = !!matchingControl.errors.misMatch;

      if (exists && count > 1) {
        delete matchingControl.errors.misMatch;
      } else if (exists && count === 1) {
        matchingControl.setErrors(null);
      }
    }
  }) as ValidatorFn;
}
