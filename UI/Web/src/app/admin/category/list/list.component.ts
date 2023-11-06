import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { AddComponent } from '../add/add.component';
import { CategoryServiceService } from 'src/app/services/category-service.service';

@Component({
  selector: 'category-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public categories : any = [];

  constructor(private modalService: NgbModal, private categoryService : CategoryServiceService) { }
  ngOnInit(): void {
    
    this.categoryService.getCategories().subscribe(
      data => {
        console.log(data);
        
        
        data.forEach((element : any) => {
          this.categories.push(element);
        });
        
      }
    )
    
  }

  openModal() {
    console.log("Open Modal");
    
    const modalRef = this.modalService.open(AddComponent);
    modalRef.result.then((result) => {  
      if(result){
        console.log(result);
      }
    }).catch((error) => {
    })
  }

}
