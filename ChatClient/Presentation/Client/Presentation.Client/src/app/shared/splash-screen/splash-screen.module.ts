import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LoadingSpinnerGearModule } from './loading-spinner-gear/loading-spinner-gear.module';
import { SplashScreenComponent } from './splash-screen.component';

@NgModule({
  declarations: [SplashScreenComponent],
  imports: [
    CommonModule,
    LoadingSpinnerGearModule
  ],
  exports: [SplashScreenComponent]
})
export class SplashScreenModule { }
