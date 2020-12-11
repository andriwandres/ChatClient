import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('@chat-client/messenger').then(m => m.MessengerModule)
  },
  {
    path: 'home',
    loadChildren: () => import('@chat-client/home').then(m => m.HomeModule)
  },
  // {
  //   path: 'auth',
  //   loadChildren: () => import('@chat-client/auth').then(m => m.AuthModule)
  // }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
