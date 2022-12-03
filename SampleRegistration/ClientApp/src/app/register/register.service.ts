import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  saveUser(user: any): Observable<any> {
    return this.http.post(this.baseUrl + 'api/registration/save', user)
      .pipe(
        catchError(this.handleError)
    );
  }
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
