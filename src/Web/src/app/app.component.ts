import { Component } from '@angular/core';
import { Patient } from 'src/models/patient.model';
import { ConvertText } from 'src/services/converttext.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private _convertText: ConvertText) {}

  title = 'Patient Records CSV Upload';

  public uploadedPatientRecords: Array<Patient> = [];

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
  public async importDataFromFileAndConvert(event: any) {
    let fileText = await this.readFileContent(event);

    this.uploadedPatientRecords = this._convertText.concertCsvToJson(fileText);
  }
}
