import { NgModule } from '@angular/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { AuthEffects } from './effects';
import { authReducer } from './reducers';
import { authFeatureKey } from './selectors';

@NgModule({
  imports: [
    StoreModule.forFeature(authFeatureKey, authReducer),
    EffectsModule.forFeature([AuthEffects]),

    RouterModule,
    MatSnackBarModule
  ],
  providers: [AuthEffects],
})
export class AuthStoreModule { }
