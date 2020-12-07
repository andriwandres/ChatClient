import { NgModule } from '@angular/core';
import { StoreModule } from '@ngrx/store';
import { authReducer } from './reducer';
import { AUTH_FEATURE_KEY } from './state';



@NgModule({
  declarations: [],
  imports: [
    StoreModule.forFeature(AUTH_FEATURE_KEY, authReducer)
  ]
})
export class AuthStoreModule { }
