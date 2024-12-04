import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { LoginModel } from '../models/loginModel';

@Injectable({
  providedIn: 'root',
})
export class AuthAPIService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(credentials: LoginModel): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Auth/login`, credentials);
  }
}
