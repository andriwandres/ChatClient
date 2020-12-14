import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
        pathMatch: 'full',
        loadChildren: () => import('@chat-client/home/about').then(m => m.AboutModule),
      },
      {
        path: 'features',
        pathMatch: 'full',
        loadChildren: () => import('@chat-client/home/features').then(m => m.FeaturesModule),
      },
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeRoutingModule { }
