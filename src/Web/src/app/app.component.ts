import { Component } from '@angular/core';
import { Patient } from 'src/models/patient.model';
import { FileUploadService } from 'src/services/fileupload.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(private _fileUploadService: FileUploadService) {}

  title = 'Patient Records CSV Upload';

  //TODO use specific type on this? maybe? what's the best practice?
  public uploadedData: Array<any> = [];

  // since javascript is, in general, a procedural language, I tend to put my methods first before being called
  // in other methods. I know that TypeScript will handle building the files and injecting them into the dumb,
  // but old habits are hard to break.
  public async readFileContent(event: any){
    const file: File = event.target.files[0];
    
    return await file.text();
  }

  public async importDataFromFile(event: any) {
    let fileText = await this.readFileContent(event);
    this.uploadedData = this._fileUploadService.importCsvFromFileUpload(fileText);
  }
}
