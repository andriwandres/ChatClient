import { FormGroup, ValidatorFn } from '@angular/forms';

/**
 * Validates two controls in a form group to have matching values
 *
 * @param controlName Name of control name
 * @param  matchingControlName Name of matching control name
 *
 * @returns Validator function used for validation
 */
export function mustMatch(controlName: string, matchingControlName: string): ValidatorFn  {
  return ((formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    // set error on matchingControl if validation fails
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({
        ...matchingControl.errors,
        misMatch: true
      });
    } else {
      matchingControl.setErrors(null);
    }
  }) as ValidatorFn;
}
