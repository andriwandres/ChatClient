import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsListComponent } from './contacts-list.component';

@NgModule({
  declarations: [ContactsListComponent],
  imports: [
    CommonModule
  ],
  exports: [ContactsListComponent]
})
export class ContactsListModule { }
