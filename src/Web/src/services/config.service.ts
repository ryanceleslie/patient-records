import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
// adding a comment here about rxjs, I know that these are React Extensions for javascript and used
// extensively for observables and Angular as a whole, more details here https://angular.io/guide/rx-library
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable()
export class ConfigService {
    constructor(private http: HttpClient) {}
}