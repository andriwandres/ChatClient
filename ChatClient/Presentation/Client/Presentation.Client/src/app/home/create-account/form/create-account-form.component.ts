import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { emailValidator } from './email.validator';
import * as errorMappings from './error-mappings';
import { minLengthValidator } from './min-length.validator';
import { mustMatch } from './password-match.validator';

@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent {
  passwordsHidden = true;

  // Getters for form controls
  get userName(): FormControl {
    return this.form.get('userName') as FormControl;
  }

  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }

  get passwordConfirm(): FormControl {
    return this.form.get('passwordConfirm') as FormControl;
  }

  // Validation rule mappings
  readonly emailMappings = errorMappings.emailMappings;
  readonly userNameMappings = errorMappings.userNameMappings;
  readonly passwordMappings = errorMappings.passwordMappings;
  readonly passwordConfirmMappings = errorMappings.passwordConfirmMappings;

  readonly form = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.pattern(/^[a-zA-Z_]\w*$/),
      minLengthValidator(2),
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.email,
      emailValidator
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(/^\S+$/),
      minLengthValidator(8),
    ]),
    passwordConfirm: new FormControl('', [
      Validators.required,
      Validators.pattern(/^\S+$/),
      minLengthValidator(8),
    ])
  }, {
    validators: [mustMatch('password', 'passwordConfirm')]
  });

  constructor(private readonly authFacade: AuthFacade) {}

  submit(): void {
    // if (this.form.valid) {
    //   const credentials = this.form.value as CreateAccountCredentials;

    //   this.authFacade.createAccount(credentials);
    // }
  }
}
