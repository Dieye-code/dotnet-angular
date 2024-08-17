import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './category/list/list.component';
import { AddComponent } from './category/add/add.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListComponent,
    AddComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [ListComponent]
})
export class AdminModule { }
