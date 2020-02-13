import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsRowComponent } from './contacts-row.component';

@NgModule({
  declarations: [ContactsRowComponent],
  imports: [
    CommonModule
  ],
  exports: [ContactsRowComponent]
})
export class ContactsRowModule { }
