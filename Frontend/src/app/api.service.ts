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

  registerPost(username: string, password: string): Observable<any> {
    const body = {
      username: username,
      password: password
    }
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    return this._http.post(this._apiUrl + "register", body, httpOptions);
  }


  // This function is temporary code delete it later
  loginGet(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': sessionStorage.getItem("Authorization") || ""
      })
    }
    return this._http.get(this._apiUrl + "login", httpOptions);
  }

  overviewGet(): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({'Authorization': sessionStorage.getItem("Authorization") || ""})
    }
    return this._http.get(this._apiUrl + "overview", httpOptions);
  }

  overviewPost(channelName: string): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': sessionStorage.getItem("Authorization") || "",
        'Content-Type': 'application/json'
      })
    }
    const body = {name: channelName};
    console.log("body", body)
    return this._http.post(this._apiUrl + "overview", body, httpOptions);
  }
}

