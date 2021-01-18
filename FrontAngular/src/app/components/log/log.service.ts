import { map, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Log } from './log.model';
import { Observable, EMPTY } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  
  baseUrl = 'https://localhost:44342/Log';

  constructor(private snackBar: MatSnackBar,
    private http: HttpClient) { }

  showMessage(msg: string, isError: boolean = false): void{
    this.snackBar.open(msg, 'fechar',
    {
      duration: 3000,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: isError ? ['msg-error'] : ['msg-success']
    })
  }

  errorHandler(error : any): Observable<any> {
    this.showMessage('Ocorreu um erro!', true);
    return EMPTY;
  }

  uploadProfilePicture(uploadedFile: FileParameter): Observable<string> {
    debugger;
    let url_ = this.baseUrl + "/UploadFIle";
    
    const content_ = new FormData();
    if (uploadedFile === null || uploadedFile === undefined)
        throw new Error("The parameter 'uploadedFile' cannot be null.");
    else
        content_.append("uploadedFile", uploadedFile.data, uploadedFile.fileName ? uploadedFile.fileName : "uploadedFile");

    let options_ : any = {
        body: content_,
        observe: "response",
        responseType: "blob",
        headers: new HttpHeaders({
            "Accept": "application/json"
        })
    };

    return this.http.request("post", url_, options_).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
}
  
  create(log: Log): Observable<Log> {
    return this.http.post<Log>(this.baseUrl, log).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
  }

  read(): Observable<Log[]> {
    return this.http.get<Log[]>(this.baseUrl).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
  }

  readById(id:string): Observable<Log> {
    const url = `${this.baseUrl}/${id}`;
    return this.http.get<Log>(url).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
  } 

  update(log: Log): Observable<Log> {
    const url = `${this.baseUrl}/${log.id}`;
    return this.http.put<Log>(url, log).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
  }

  delete(id:string): Observable<Log> {
    const url = `${this.baseUrl}/${id}`;
    return this.http.delete<Log>(url).pipe(
      map(obj => obj),
      catchError(err => this.errorHandler(err))
    );
  }
}

export interface FileParameter {
  data: any;
  fileName: string;
}