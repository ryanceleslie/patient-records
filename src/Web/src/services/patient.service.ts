import { Injectable } from "@angular/core";  
import { HttpClient } from "@angular/common/http";  
import { Observable, throwError } from "rxjs";  
import { catchError } from 'rxjs/operators';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

// Custom Imports
import { Patient } from "src/models/patient.model";


@Injectable({
  providedIn: "root"
})

export class PatientService {
  private url = 'https://patient-records.azurewebsites.net/';
  //private url = 'https://localhost:7271/';

  constructor(private _http: HttpClient) {}

  getPatientRecords(): Observable<Patient[]> {
    return this._http.get<Patient[]>(this.url + 'api/patient').pipe(catchError(this.handleError));
  }

  postBatchPatientRecords(patients: Patient[]): Observable<any> {
    return this._http.post(this.url + 'api/patient/batch', patients).pipe(catchError(this.handleError));
  }

  putPatientRecord(patient: Patient): Observable<any> {
    return this._http.put(this.url + 'api/patient/' + patient.id, patient).pipe(catchError(this.handleError));
  }

  // this could be a utility to use elsewhere in the project and something I could move to a custom
  // handling error service, particularly for logging
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}