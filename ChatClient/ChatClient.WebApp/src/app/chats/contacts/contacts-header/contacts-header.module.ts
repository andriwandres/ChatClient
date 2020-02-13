import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsHeaderComponent } from './contacts-header.component';



@NgModule({
  declarations: [ContactsHeaderComponent],
  imports: [
    CommonModule
  ],
  exports: [ContactsHeaderComponent]
})
export class ContactsHeaderModule { }
