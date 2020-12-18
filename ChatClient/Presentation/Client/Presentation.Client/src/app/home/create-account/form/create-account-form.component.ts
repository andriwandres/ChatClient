import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from './password-match.validator';

@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent implements OnInit {
  passwordsHidden = true;

  get userName(): AbstractControl {
    return this.form.get('userName') as AbstractControl;
  }

  get email(): AbstractControl {
    return this.form.get('email') as AbstractControl;
  }

  get password(): AbstractControl {
    return this.form.get('password') as AbstractControl;
  }

  get passwordConfirm(): AbstractControl {
    return this.form.get('passwordConfirm') as AbstractControl;
  }

  readonly form = new FormGroup({
    userName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.pattern(/^[a-zA-Z_]\w*$/)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.email
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

  constructor() { }

  ngOnInit(): void {
  }

}
