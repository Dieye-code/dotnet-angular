import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  @Input() public category: any;
  //@Output() private passEntry = new EventEmitter<any>();

  form = new FormGroup({
    id: new FormControl(null),
    libelle: new FormControl('', {
      validators: Validators.required
    })
  });

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    if (this.category != undefined) {
      this.form.setValue({ id: this.category['id'], libelle: this.category['libelle'] });
    } else {
      this.category = {
        id: '',
        libelle: ''
      }
    }

  }

  submit() {
    this.category.libelle = this.form.controls.libelle.value ?? ''
    this.activeModal.close(this.category);
  }

  reset() {
    this.activeModal.dismiss("cross click");
  }

}
