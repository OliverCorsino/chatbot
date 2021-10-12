import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRequest } from './../../models/auth-request';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.buildForm();
  }

  login(): void {
    const authRequest = this.loginForm.getRawValue() as AuthRequest;

    this.authService.login(authRequest).subscribe((result: any) => {
      localStorage.setItem('token', result.token);
      this.router.navigate(['']);
    }, (error: HttpErrorResponse) => {
      console.log(error);
    });
  }

  navigateToSignUp(): void {
    this.router.navigate(['sign-up']);
  }

  private buildForm(): void {
    this.loginForm = this.formBuilder.group({
      username: new FormControl('', [Validators.required, Validators.maxLength(30)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(30)])
    });
  }

}
