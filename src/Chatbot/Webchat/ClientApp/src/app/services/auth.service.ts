import { SignUpRequest } from './../models/sign-up-request';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient , @Inject('BASE_URL') private baseUrl: string) { }

  signUp(signUpRequest: SignUpRequest) {
    return this.http.post(`${this.baseUrl}api/auth/sign-up`, signUpRequest);
  }
}
