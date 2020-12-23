import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateAccountCredentials } from '@chat-client/core/models';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { Action } from '@ngrx/store';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, skipWhile, takeUntil } from 'rxjs/operators';
import { emailValidator } from './email.validator';
import * as errorMappings from './error-mappings';
import { minLengthValidator } from './min-length.validator';
import { mustMatch } from './password-match.validator';

@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent implements OnInit, OnDestroy {
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

  // Store selectors
  readonly emailExists$ = this.authFacade.emailExists$;
  readonly userNameExists$ = this.authFacade.userNameExists$;

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

  private readonly destroy$ = new Subject();

  constructor(private readonly authFacade: AuthFacade) {}

  ngOnInit(): void {
    const DEBOUNCE_TIME = 1000;

    // Check user name availability
    this.userName.valueChanges.pipe(
      skipWhile(() => this.userName.invalid),
      debounceTime(DEBOUNCE_TIME),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((userName: string) => this.authFacade.checkUserNameExists(userName));

    // Check email availability
    this.email.valueChanges.pipe(
      skipWhile(() => this.email.invalid),
      debounceTime(DEBOUNCE_TIME),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((email: string) => this.authFacade.checkEmailExists(email));

    // Set error on email unavailability
    this.emailExists$.pipe(
      takeUntil(this.destroy$)
    ).subscribe(exists => {
      const errors = exists ? { emailExists: true } : null;

      this.email.setErrors(errors);
    });

    // Set error on email unavailability
    this.userNameExists$.pipe(
      takeUntil(this.destroy$)
    ).subscribe(exists => {
      const errors = exists ? { userNameExists: true } : null;

      this.userName.setErrors(errors);
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit(): void {
    if (this.form.valid) {
      const credentials = this.form.value as CreateAccountCredentials;

      this.authFacade.createAccount(credentials);
    }
  }
}
