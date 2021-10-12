import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthRequest } from './../models/auth-request';
import { SignUpRequest } from './../models/sign-up-request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private jwtHelper: JwtHelperService) { }

  signUp(signUpRequest: SignUpRequest) {
    return this.http.post(`${this.baseUrl}api/auth/sign-up`, signUpRequest);
  }

  login(authRequest: AuthRequest) {
    return this.http.post(`${this.baseUrl}api/auth/sign-in`, authRequest);
  }

  logout() {
    localStorage.removeItem('token');

    this.router.navigate(['']);
  }

  isAuthenticated() {
    return localStorage.getItem('token') !== null && !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }
}
