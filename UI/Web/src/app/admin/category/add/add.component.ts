import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  @Input() public categorie = {id: null, libelle: ''};
  //@Output() private passEntry = new EventEmitter<any>();

  form = new FormGroup({
    id: new FormControl(null),
    libelle: new FormControl('', {
      validators: Validators.required
    })
  });

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit(): void {

  }

  submit() {
    this.categorie.libelle = this.form.controls.libelle.value ?? ''
    this.activeModal.close(this.categorie);
  }

  reset() {
    this.activeModal.dismiss("cross click");
  }

}
