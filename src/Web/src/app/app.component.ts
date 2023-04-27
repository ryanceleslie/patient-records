import { Component, ViewChild } from '@angular/core';
import { Observable } from 'rxjs'

// Material Imports
import { MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

// Custom Imports
import { Patient } from 'src/models/patient.model';
import { PatientService } from 'src/services/patient.service';
import { ConvertTextService } from 'src/services/converttext.service';
import { PatientDetailsComponent } from './patient-details/patient-details.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = "Patient Records Database";

  public uploadedPatientRecords: Patient[] = [];
  public toggleErrorReponseVisibilty: boolean = false;
  public toggleReponseVisibilty: boolean = false;
  public responseMessage: string = "";

  public existingPatients!: MatTableDataSource<Patient>;
  public displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'gender', 'id'];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _patientService: PatientService,
    private _convertTextService: ConvertTextService,
    public dialog: MatDialog) {

    // load existing patient records      
    this._patientService.getPatientRecords()
      .subscribe((patients: Patient[]) => {
        this.existingPatients = new MatTableDataSource<Patient>(patients);

        // Material's documentation has this in the ngAfterViewInit but the paginator and sort properties would be null or empty
        // this only works in the constructor, which makes sense given that this should be deffered until the data is populated 
        // into the existing records table
        this.existingPatients.paginator = this.paginator;
        this.existingPatients.sort = this.sort;
      });
  }

  public async resetResponse() {
    // Reset responses
    this.uploadedPatientRecords = [];
    this.toggleErrorReponseVisibilty = false;
    this.toggleReponseVisibilty = false;
    this.responseMessage = "";
  }

  // since javascript is, in general, a procedural language, I tend to put my methods first before being called
  // in other methods. I know that TypeScript will handle building the files and injecting them into the dumb,
  // but old habits are hard to break.
  public async readFileContent(event: any) {
    const file: File = event.target.files[0];

    return await file.text();
  }

  // Normally, I would prefer to use a standard library to parse text from one construct to another, a library
  // like PapaParse is something that would work well, however, for the purposes of this exercise, I wrote my
  // own service class to handle this in a rudimentary form.
  public async importDataFromFile(event: any) {
    let fileText = await this.readFileContent(event);

    // I generally create single-use variables when trying to make readable code. In this case, I didn't want
    // to pass an await on a method as a parameter into the patient service. It would make the code a little
    // more challenging to read
    var convertedJson = await this._convertTextService.csvToJson(fileText);

    //TODO I think I need to remove this response reset and make it an observable?
    await this.resetResponse().then(() => {
      if (convertedJson.hasOwnProperty("Error")) {
        // Display the error response div
        this.toggleErrorReponseVisibilty = true;
        this.responseMessage = convertedJson["Error"].toString();
      }
      else {
        this._patientService.postBatchPatientRecords(convertedJson)
          .subscribe(response => {

            // Setting the response data into the display table for data just entered instead of the data pulled
            // from the csv file. This confirms that it's data from the post response, not prior.
            this.uploadedPatientRecords = response;

            // Display the response div
            this.toggleReponseVisibilty = true;
            this.responseMessage = "Here is the data that was imported into the system";

            // reload the existing patient records
            this._patientService.getPatientRecords()
              .subscribe((patients: Patient[]) => {
                this.existingPatients = new MatTableDataSource<Patient>(patients);
                this.existingPatients.paginator = this.paginator;
                this.existingPatients.sort = this.sort;
              });
          });
      }
    });


  }

  public async editPatient(patient: Patient) {
    const dialogRef = this.dialog.open(PatientDetailsComponent, {
      width: '300px',
      data: patient
    });

    dialogRef.afterClosed().subscribe(patient => {

      // need this check here in case they hit the cancel button as the .afterClosed() event is executed
      if (patient != undefined) {
        // Send an HTTP PUT to update, I feel like this should be 
        this._patientService.putPatientRecord(patient)
          .subscribe();
      }
    });
  }

  public async applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.existingPatients.filter = filterValue.trim().toLowerCase();

    if (this.existingPatients.paginator) {
      this.existingPatients.paginator.firstPage();
    }
  }
}
