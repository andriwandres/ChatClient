import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';

@NgModule({
  imports: [
    // StoreModule.forFeature('', null),
    EffectsModule.forFeature([])
  ]
})
export class MessagesStoreModule {}
