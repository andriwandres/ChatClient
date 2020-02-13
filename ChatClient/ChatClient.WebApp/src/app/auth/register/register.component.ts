import { animate, state, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { takeUntil, distinctUntilChanged, debounceTime, skipWhile } from 'rxjs/operators';
import { Store, select } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';
import { AuthStoreActions, AuthStoreSelectors } from 'src/app/app-store/auth-store';
import { MustMatch } from './password-match.validator';
import { RegisterDto } from 'src/models/auth/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  animations: [
    trigger('fadeIn', [
      state('void', style({
        opacity: 0,
        transform: 'translateY(-25px)'
      })),
      transition(':enter', animate(500)),
    ]),
  ]
})
export class RegisterComponent implements OnInit, OnDestroy {
  @HostBinding('@fadeIn') readonly animationBinding: string;

  private readonly destroy$ = new Subject<void>();

  readonly emailTaken$ = this.store$.pipe(
    select(AuthStoreSelectors.selectEmailTaken),
    takeUntil(this.destroy$),
  );

  readonly isLoading$ = this.store$.pipe(
    select(AuthStoreSelectors.selectLoading),
    takeUntil(this.destroy$),
  );

  readonly form = new FormGroup({
    displayName: new FormControl('', [
      Validators.required,
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^\S+$/)
    ]),
    passwordConfirm: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^\S+$/)
    ]),
  }, {
    validators: MustMatch('password', 'passwordConfirm')
  });

  get displayName() { return this.form.get('displayName'); }
  get email() { return this.form.get('email'); }
  get password() { return this.form.get('password'); }
  get passwordConfirm() { return this.form.get('passwordConfirm'); }

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    this.email.valueChanges.pipe(
      skipWhile(() => this.email.invalid),
      debounceTime(1000),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe((email: string) => {
      this.store$.dispatch(AuthStoreActions.checkEmailAvailability({ email }));
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  submit(): void {
    if (this.form.valid) {
      const credentials: RegisterDto = this.form.value;

      this.store$.dispatch(AuthStoreActions.register({ credentials }));
    }
  }
}
