import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private baseUrl = "http://localhost:3060/api/"; 
  constructor(private httpClient: HttpClient) { }
  postFile(fileToUpload: File): Observable<boolean> {
    const reqHeader = new HttpHeaders()
    .set('Content-Type','application/json')
    .set('Accept','application/json');
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    return null;
    /*this.httpClient
      .post(this.baseUrl, formData, { headers: reqHeader })
      .pipe(
        tap((data) => {
          if(data)
          {
            alert('Submitted new Asset into database');
          }
          else
          {
            alert('Not able to insert New Asset; Check with Admin');
          }
        }),
        catchError(this.handleError)
      );
      .catch((e) => this.handleError(e));*/
}
handleError(err: any): Observable<never> {
  let errorMessage: string;
  if(err.error instanceof ErrorEvent)
  {
    errorMessage=`An Error occured : ${err.error.message}`;
  }
  else
  {
    errorMessage = `Backend returned code ${err.Status}: ${err.body.error}`;
  }
  return throwError(errorMessage);
}
}
