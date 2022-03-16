import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { AuthEffects } from './auth.effects';
import { authReducer } from './auth.reducer';
import { AUTH_FEATURE_KEY } from './auth.state';

@NgModule({
  declarations: [],
  imports: [
    StoreModule.forFeature(AUTH_FEATURE_KEY, authReducer),
    EffectsModule.forFeature([AuthEffects]),
  ],
  providers: [AuthEffects]
})
export class AuthStoreModule { }
