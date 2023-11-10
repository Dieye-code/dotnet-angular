import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryServiceService {

  constructor(private http : HttpClient) { }

  private env = environment

  getCategories() : Observable<any> {
    return this.http.get(this.env.api+"categories");
  }

  addCategory(category: any) : Observable<any>{
    return this.http.post(this.env.api+"categories",{libelle: category.libelle});
  }

  editCategory(category: any) : Observable<any>{
    return this.http.put(this.env.api+"categories",{id: category.id, libelle: category.libelle});
  }

  deleteCategory(category: any) : Observable<any>{
    return this.http.delete(this.env.api+"categories/"+category.id);
  }


  


}
