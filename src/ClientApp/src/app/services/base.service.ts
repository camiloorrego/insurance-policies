import { DataProvider } from './../providers/data.provider';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient, public data: DataProvider) { }

  post(body: any, url: string, setToken: boolean = false) {

    const options: any = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
    };

    if (setToken) {
      options.headers = options.headers.append('Authorization', `Bearer ${this.data.token}`);

    }

    return this.http.post(url, body, options);
  }

  put(body: any, url: string, setToken: boolean = false) {
    const options: any = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
    };

    if (setToken) {
      options.headers = options.headers.append('Authorization', `Bearer ${this.data.token}`);

    }

    return this.http.put(url, body, options);
  }

  get(url: string, setToken: boolean = false) {

    const options: any = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
    };

    if (setToken) {
      options.headers = options.headers.append('Authorization', `Bearer ${this.data.token}`);

    }


    return this.http.get(url, options);
  }


  delete(url: string, setToken: boolean = false) {
    const options: any = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
    };

    if (setToken) {
      options.headers = options.headers.append('Authorization', `Bearer ${this.data.token}`);

    }


    return this.http.delete(url, options);
  }

}

