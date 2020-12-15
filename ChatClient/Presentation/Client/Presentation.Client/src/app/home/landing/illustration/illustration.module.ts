import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingIllustrationComponent } from './illustration.component';
import { IllustrationBuilderModule } from 'src/app/shared/illustration-builder/illustration-builder.module';

@NgModule({
  declarations: [LandingIllustrationComponent],
  imports: [
    CommonModule,
    IllustrationBuilderModule
  ],
  exports: [LandingIllustrationComponent]
})
export class LandingIllustrationModule { }
