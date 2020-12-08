import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getRequest(url: string): Observable<any> {
    return this.http.get(this.baseUrl + url);
  }

  postRequest(url: string, header: string, body: object): Observable<any> {
    return this.http.post(this.baseUrl + url, body, {
      headers: {
        header: header
      }
    });
  }
}
