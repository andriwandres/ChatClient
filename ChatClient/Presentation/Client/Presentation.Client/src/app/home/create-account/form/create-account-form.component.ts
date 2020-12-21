import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { emailValidator } from './email.validator';
import { MustMatch } from './password-match.validator';
import * as ruleMappings from './rule-mappings';

@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent {
  passwordsHidden = true;

  get userName(): FormControl { return this.form.get('userName') as FormControl; }
  get email(): FormControl { return this.form.get('email') as FormControl; }
  get password(): FormControl { return this.form.get('password') as FormControl; }
  get passwordConfirm(): FormControl { return this.form.get('passwordConfirm') as FormControl; }

  readonly emailMappings = ruleMappings.emailMappings;
  readonly userNameMappings = ruleMappings.userNameMappings;
  readonly passwordMappings = ruleMappings.passwordMappings;
  readonly passwordConfirmMappings = ruleMappings.passwordConfirmMappings;

  readonly form = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern(/^[a-zA-Z_]\w*$/)
    ]),
    email: new FormControl('', [
      Validators.required,
      emailValidator
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
    passwordConfirm: new FormControl('', [
      Validators.required,
    ])
  }, {
    validators: [MustMatch('password', 'passwordConfirm')]
  });

  constructor() {

  }
}
