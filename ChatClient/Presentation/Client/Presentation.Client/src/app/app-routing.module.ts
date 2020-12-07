import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('@chat-client/home').then(m => m.HomeModule)
  },
  {
    path: 'app',
    loadChildren: () => import('@chat-client/messenger').then(m => m.MessengerModule)
  },
  {
    path: 'auth',
    loadChildren: () => import('@chat-client/auth').then(m => m.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
