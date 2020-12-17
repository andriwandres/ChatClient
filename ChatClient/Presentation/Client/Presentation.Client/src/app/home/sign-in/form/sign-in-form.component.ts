import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiError, LoginCredentials } from '@chat-client/core/models';
import { AuthFacade } from '@chat-client/shared/auth/store';
import { merge, Subject } from 'rxjs';
import { filter, skip, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.scss'],
})
export class SignInFormComponent implements OnInit, OnDestroy {
  get userNameOrEmail(): AbstractControl {
    return this.form.get('userNameOrEmail') as AbstractControl;
  }

  get password(): AbstractControl {
    return this.form.get('password') as AbstractControl;
  }

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
    filter(value => !!value),
    takeUntil(this.destroy$)
  );

  private readonly error$ = this.authFacade.loginError$.pipe(
    takeUntil(this.destroy$)
  );

  constructor(private readonly authFacade: AuthFacade) { }

  ngOnInit(): void {
    // Remove errors, when value is being updated
    this.update$.subscribe(value => {
      if (value) {
        this.password.setErrors(null);
      }
    });

    // Display an error when credentials are wrong
    this.error$.subscribe(error => {
      if (error && error.statusCode === 401) {
        this.password.setErrors({ credentialsIncorrect: true });
      }
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
