import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HomePageHeaderModule } from './home-page-header/home-page-header.module';
import { HomeRoutingModule } from './home-routing.module';
import { HomeComponent } from './home.component';

@NgModule({
  declarations: [HomeComponent],
  imports: [
    CommonModule,
    HomeRoutingModule,
    HomePageHeaderModule
  ]
})
export class HomeModule { }
