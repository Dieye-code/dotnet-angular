import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './category/list/list.component';
import { AddComponent } from './category/add/add.component';



@NgModule({
  declarations: [
    ListComponent,
    AddComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [ListComponent]
})
export class AdminModule { }
