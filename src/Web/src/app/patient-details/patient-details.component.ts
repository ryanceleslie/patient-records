import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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
  public patientForm: FormGroup<any>;

  constructor(
    public dialogRef: MatDialogRef<PatientDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Patient) {
      this.patientForm = new FormGroup({
        fristName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        dateOfBirth: new FormControl(new Date()),
        gender: new FormControl('', [Validators.required, Validators.maxLength(2)])
      });

    }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.patientForm.controls[controlName].hasError(errorName);
  }
}
