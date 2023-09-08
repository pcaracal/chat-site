import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private _apiUrl: string = "http://localhost:5147/api/";

  constructor(private _http: HttpClient) {
  }

  loginPost(username: string, password: string): Observable<any> {
    const body = {
      username: username,
      password: password
    }
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this._http.post(this._apiUrl + "login", body, httpOptions);
  }
}
