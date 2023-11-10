import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { AddComponent } from '../add/add.component';
import { CategoryServiceService } from 'src/app/services/category-service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'category-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  public categories: any = [];

  constructor(private modalService: NgbModal, private categoryService: CategoryServiceService) { }
  ngOnInit(): void {

    this.categoryService.getCategories().subscribe(
      data => {
        data.forEach((element: any) => {
          this.categories.push(element);
        });
      }
    )
  }

  openModal() {
    const modalRef = this.modalService.open(AddComponent);
    modalRef.result.then((result) => {
      if (result) {
        this.categoryService.addCategory(result).subscribe(
          data => {
            this.categories.push(data)
          }
        );
      }
    }).catch((error) => {
    })
  }

  edit(category: any) {
    const modalRef = this.modalService.open(AddComponent);
    modalRef.componentInstance.category = category;
    modalRef.result.then((result) => {
      if (result) {
        this.categoryService.editCategory(result).subscribe(
          data => {
            let indexToUpdate = this.categories.findIndex((item: any) => item.id === data.id);
            this.categories[indexToUpdate] = data;
            this.categories = Object.assign([], this.categories);
          }
        );
      }
    }).catch((error) => {
    })
  }

  delete(category: any) {
    Swal.fire({
      title: "Voulez-vous supprimer ce catégorie?",
      showDenyButton: true,
      confirmButtonText: "Supprimer",
      denyButtonText: `Annuler`
    }).then((result) => {
      if (result.isConfirmed) {
        this.categoryService.deleteCategory(category).subscribe(
          (result) => {
            let indexToUpdate = this.categories.findIndex((item: any) => item.id === category.id);
            if (indexToUpdate != -1)
              this.categories.pop(indexToUpdate);
            Swal.fire("Succés!", "La catégorie a été bien supprimé", "success");
          },
          error => {
            console.log('Error', error);
          }
        );
      }
    });
  }

}
