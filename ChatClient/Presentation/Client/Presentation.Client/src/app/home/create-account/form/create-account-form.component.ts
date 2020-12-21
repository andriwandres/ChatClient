import { Component } from '@angular/core';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { CreateAccountCredentials } from 'src/app/core/models/user';
import { emailValidator } from './email.validator';
import { minLengthValidator } from './min-length.validator';
import { mustMatch } from './password-match.validator';
import * as ruleMappings from './rule-mappings';

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
  readonly emailMappings = ruleMappings.emailMappings;
  readonly userNameMappings = ruleMappings.userNameMappings;
  readonly passwordMappings = ruleMappings.passwordMappings;
  readonly passwordConfirmMappings = ruleMappings.passwordConfirmMappings;

  readonly form = new FormGroup({
    userName: new FormControl('', Validators.compose([
      Validators.required,
      Validators.pattern(/^[a-zA-Z_]\w*$/),
      minLengthValidator(2),
    ])),
    email: new FormControl('', Validators.compose([
      Validators.required,
      emailValidator
    ])),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(/^\S+$/),
      minLengthValidator(8),
    ]),
    passwordConfirm: new FormControl('', [
      Validators.required,
      Validators.pattern(/^\S+$/),
      minLengthValidator(8)
    ])
  }, {
    validators: [mustMatch('password', 'passwordConfirm')]
  });

  constructor(private readonly authFacade: AuthFacade) {}

  submit(): void {
    if (this.form.valid) {
      const credentials = this.form.value as CreateAccountCredentials;

      this.authFacade.createAccount(credentials);
    }
  }
}
