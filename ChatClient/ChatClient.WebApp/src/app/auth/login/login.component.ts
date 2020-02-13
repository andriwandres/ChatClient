import { animate, state, style, transition, trigger } from '@angular/animations';
import { ChangeDetectionStrategy, Component, HostBinding } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';
import { AuthStoreActions } from 'src/app/app-store/auth-store';
import { LoginDto } from 'src/models/auth/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
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
export class LoginComponent {
  @HostBinding('@fadeIn') readonly animationBinding: string;

  readonly form = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.pattern(/^\S+$/),
    ])
  });

  get email() { return this.form.get('email'); }
  get password() { return this.form.get('password'); }

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  submit(): void {
    const credentials: LoginDto = this.form.value;

    if (this.form.valid) {
      this.store$.dispatch(AuthStoreActions.login({ credentials }));
    }
  }
}
