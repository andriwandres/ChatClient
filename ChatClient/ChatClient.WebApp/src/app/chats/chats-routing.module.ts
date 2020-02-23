import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatsComponent } from './chats.component';

const routes: Routes = [
  {
    path: '',
    component: ChatsComponent,
    children: [
      {
        path: 'user/:id',
        data: { isGroupChat: false },
        loadChildren: () => import('./chat/chat.module').then(m => m.ChatModule),
      },
      {
        path: 'group/:id',
        data: { isGroupChat: true },
        loadChildren: () => import('./chat/chat.module').then(m => m.ChatModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChatsRoutingModule { }
