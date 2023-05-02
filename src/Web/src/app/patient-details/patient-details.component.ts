import { Component, Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

// Material Imports
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

// Custom Imports
import { Patient } from 'src/models/patient.model';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.scss']
})
export class PatientDetailsComponent {
  public patientDetailsEditForm: FormGroup;

  constructor(fb: FormBuilder, public dialogRef: MatDialogRef<PatientDetailsComponent>, @Inject(MAT_DIALOG_DATA) public patient: Patient) 
  {
    this.patientDetailsEditForm = fb.group({
      firstName: new FormControl(patient.firstName, [Validators.required]),
      lastName: new FormControl(patient.lastName, [Validators.required]),
      dateOfBirth: new FormControl(patient.dateOfBirth, [Validators.required]),
      gender: new FormControl(patient.gender, [Validators.required, Validators.maxLength(2)])
    });
  }

  public hasError = (controlName: string, errorName: string) =>{
    return this.patientDetailsEditForm.controls[controlName].hasError(errorName);
  }

  onUpdateClick() {
    const {value, valid} = this.patientDetailsEditForm;
    if(valid){
      this.dialogRef.close(value);
    }
}

  onCancelClick(): void {
    this.dialogRef.close();
  }
}
