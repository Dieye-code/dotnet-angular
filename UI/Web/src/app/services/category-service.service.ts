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


  


}
