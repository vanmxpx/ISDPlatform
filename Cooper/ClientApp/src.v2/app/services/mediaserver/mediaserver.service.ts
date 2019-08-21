import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MediaserverService {

  constructor(private httpClient: HttpClient) { }

  public uploadImage(file: File, type: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post('/media/' + type, formData);
  }
}
