import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateAccountGuard } from '../core/guards/create-account.guard';
import { SignInGuard } from '../core/guards/sign-in.guard';
import { HomeComponent } from './home.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        loadChildren: () => import('@chat-client/home/landing').then(m => m.LandingModule),
      },
      {
        path: 'about',
        loadChildren: () => import('@chat-client/home/about').then(m => m.AboutModule),
      },
      {
        path: 'features',
        loadChildren: () => import('@chat-client/home/features').then(m => m.FeaturesModule),
      },
      {
        path: 'sign-in',
        canLoad: [SignInGuard],
        canActivate: [SignInGuard],
        loadChildren: () => import('@chat-client/home/sign-in').then(m => m.SignInModule),
      },
      {
        path: 'create-account',
        canLoad: [CreateAccountGuard],
        canActivate: [CreateAccountGuard],
        loadChildren: () => import('@chat-client/home/create-account').then(m => m.CreateAccountModule)
      }
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
