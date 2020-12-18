import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateAccountIllustrationComponent } from './create-account-illustration.component';
import { IllustrationBuilderModule } from '@chat-client/shared/illustration-builder';

@NgModule({
  declarations: [CreateAccountIllustrationComponent],
  imports: [
    CommonModule,
    IllustrationBuilderModule
  ],
  exports: [CreateAccountIllustrationComponent]
})
export class CreateAccountIllustrationModule { }
