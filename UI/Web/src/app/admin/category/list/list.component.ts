import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { AddComponent } from '../add/add.component';

@Component({
  selector: 'category-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {
  constructor(private modalService: NgbModal) { }

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
