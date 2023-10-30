import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './admin/category/list/list.component';

const routes: Routes = [
  {
    path: 'admin',
    children: [
      {
        path: 'categories',
        component: ListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
