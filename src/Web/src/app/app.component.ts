import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs'

// Material Imports
import { MatTableDataSource } from '@angular/material/table';
import { MatFormField } from '@angular/material/form-field';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

// custom imports
import { Patient } from 'src/models/patient.model';
import { PatientService } from 'src/services/patient.service';
import { ConvertTextService } from 'src/services/converttext.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public existingpatients: Patient[] = [];
  public uploadedPatientRecords: Patient[] = [];
  public toggleReponseVisibilty: boolean = false;

  public existingPatients!: MatTableDataSource<Patient>;
  public displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'gender', 'id'];

  constructor(private _patientService: PatientService, private _convertTextService: ConvertTextService, private _httpClient: HttpClient) {}

  ngOnInit() {
    // load existing patient records
    //TODO cleanup
    this._patientService.getPatientRecords()
      .subscribe(patients => (this.existingpatients = patients));

      
    this._patientService.getPatientRecords()
      .subscribe(patients => (this.existingPatients = new MatTableDataSource<Patient>(patients)));

      var temp = this.existingpatients;
  }

  // since javascript is, in general, a procedural language, I tend to put my methods first before being called
  // in other methods. I know that TypeScript will handle building the files and injecting them into the dumb,
  // but old habits are hard to break.
  public async readFileContent(event: any){
    const file: File = event.target.files[0];
    
    return await file.text();
  }

  // Normally, I would prefer to use a standard library to parse text from one construct to another, a library
  // like PapaParse is something that would work well, however, for the purposes of this exercise, I wrote my
  // own service class to handle this in a rudimentary form.
  public async importDataFromFile(event: any) {
    let fileText = await this.readFileContent(event);

    // I generally create single-use variables when trying to make readable code. In this case, I didn't want
    // to put pass an await on a method as a parameter into the patient service. It would make the code a little
    // more challenging to read
    var convertedJson = await this._convertTextService.csvToJson(fileText);

    this._patientService.postBatchPatientRecords(convertedJson)
      .subscribe(response => {

        // Setting the response data into the display table for data just entered instead of the data pulled
        // from the csv file. This confirms that it's data from the post response, not prior.
        this.uploadedPatientRecords = response;

        // Display the response div
        this.toggleReponseVisibilty = true;

        // reload the existing patient records
        this._patientService.getPatientRecords()
          .subscribe(patients => (this.existingpatients = patients));
      });
  }

  public async updatePatient(event: any){
    return null;
  }

  public async applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.existingPatients.filter = filterValue.trim().toLowerCase();
  }
}
