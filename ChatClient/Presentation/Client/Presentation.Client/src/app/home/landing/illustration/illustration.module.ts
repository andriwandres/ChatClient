import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IllustrationComponent } from './illustration.component';
import { IllustrationBuilderModule } from 'src/app/shared/illustration-builder/illustration-builder.module';

@NgModule({
  declarations: [IllustrationComponent],
  imports: [
    CommonModule,
    IllustrationBuilderModule
  ],
  exports: [IllustrationComponent]
})
export class IllustrationModule { }
