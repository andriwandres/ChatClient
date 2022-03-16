import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { LoginCredentials } from '@chat-client/core/models';
import { Subject } from 'rxjs';
import { filter, map, takeUntil } from 'rxjs/operators';
import { AuthFacade } from 'src/app/shared/store/auth';
import * as errorMappings from './error-mapings';

@Component({
  selector: 'app-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.scss'],
})
export class SignInFormComponent implements OnInit, OnDestroy {
  get userNameOrEmail(): FormControl {
    return this.form.get('userNameOrEmail') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }

  readonly userNameOrEmailMappings = errorMappings.userNameOrEmailMapping;
  readonly passwordMappings = errorMappings.passwordMapping;

  readonly form = new FormGroup({
    userNameOrEmail: new FormControl('', [
      Validators.required,
    ]),
    password: new FormControl('', [
      Validators.required,
    ])
  });

  private readonly destroy$ = new Subject();

  private readonly update$ = this.form.valueChanges.pipe(
    map(value => value as LoginCredentials),
    takeUntil(this.destroy$)
  );

  private readonly error$ = this.authFacade.loginError$.pipe(
    filter(error => !!error && error.statusCode === 401),
    takeUntil(this.destroy$),
  );

  constructor(private readonly authFacade: AuthFacade) { }

  ngOnInit(): void {
    // Remove errors, when value is being updated
    this.update$.subscribe(value => {
      if (value.password) {
        this.password.setErrors(null);
      }
    });

    // Display an error when credentials are wrong
    this.error$.subscribe(() => {
      this.password.setErrors({ credentialsIncorrect: true });
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit(): void {
    if (this.form.valid) {
      const credentials = this.form.value as LoginCredentials;

      this.authFacade.logIn(credentials);
    }
  }
}
