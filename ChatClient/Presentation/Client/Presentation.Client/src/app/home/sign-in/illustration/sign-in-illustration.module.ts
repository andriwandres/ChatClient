import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { IllustrationBuilderModule } from '@chat-client/shared/illustration-builder';
import { SignInIllustrationComponent } from './sign-in-illustration.component';

@NgModule({
  declarations: [SignInIllustrationComponent],
  imports: [
    CommonModule,
    IllustrationBuilderModule,
  ],
  exports: [SignInIllustrationComponent]
})
export class SignInIllustrationModule { }
