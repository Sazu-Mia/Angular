import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { CandidateInputModel } from '../../../models/view-model/candidate-input-model';
import { CandidateService } from '../../../services/data/candidate.service';

@Component({
  selector: 'app-candidate-create',
  templateUrl: './candidate-create.component.html',
  styleUrls: ['./candidate-create.component.css']
})
export class CandidateCreateComponent implements OnInit {
 
  candidate: CandidateInputModel = {};
  file: File = null!;
  candidateForm: FormGroup = new FormGroup({

    candidateName: new FormControl('', Validators.required),
    birthDate: new FormControl(undefined, Validators.required),
    appliedFor: new FormControl('', Validators.required),
    expectedSalary: new FormControl(undefined, Validators.required),
    workFromHome: new FormControl(undefined),
    picture: new FormControl('', Validators.required),
    qualifications: new FormArray([])
  });
  get f() {
    return this.candidateForm.controls;
  }
  get qualicationsFormArray() {
    return this.candidateForm.controls["qualifications"] as FormArray;
  }
  constructor(
    private candidateSvc: CandidateService,
    private datePipe: DatePipe
  ) { }
  save() {
    if (this.candidateForm.invalid) return;
    Object.assign(this.candidate, this.candidateForm.value);
    this.candidate.birthDate = <string>this.datePipe.transform(this.candidate.birthDate, 'yyyy-MM-dd');
    //console.log(this.candidate);
    this.candidateSvc.post(this.candidate)
      .subscribe({
        next: r => {
          this.uploadImage(<number>r.candidateId)
        },
        error: err => {
          console.log(err.message || err);
        }
      });

  }
  uploadImage(id: number) {
    var reader = new FileReader();
    var _self = this;
    reader.onload = function (e: any) {
      console.log(e);
      _self.candidateSvc.uploadImage(id, _self.file)
        .subscribe({
          next: r => {
            console.log(r);
            console.log("Saved")
          },
          error: err => {
            console.log('Picture upload failed', 'DISMISS');
          }
        });
    }
    reader.readAsArrayBuffer(_self.file);
  }
  addItem() {
    this.qualicationsFormArray.push(new FormGroup({
      degree: new FormControl('', Validators.required),
      institute: new FormControl('', Validators.required),
      passingYear: new FormControl(undefined, Validators.required),
      result: new FormControl('', Validators.required)
    }));
  }
  removeItem(index: number) {
    if (this.qualicationsFormArray.controls.length > 1)
      this.qualicationsFormArray.removeAt(index);
  }
  ngOnInit(): void {
    this.addItem();
  }
  handleFileInputChange(event: any): void {
    if (event.target.files.length) {
      this.file = event.target.files[0];
      this.candidateForm.controls['picture'].patchValue(this.file.name);
    }
    else {
      this.candidateForm.controls['picture'].patchValue("");
    }

  }
}
