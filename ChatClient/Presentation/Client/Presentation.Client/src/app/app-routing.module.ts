import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeGuard, MessengerGuard } from '@chat-client/core/guards';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    canLoad: [MessengerGuard],
    canActivate: [MessengerGuard],
    loadChildren: () => import('@chat-client/messenger').then(m => m.MessengerModule),
  },
  {
    path: 'home',
    loadChildren: () => import('@chat-client/home').then(m => m.HomeModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
