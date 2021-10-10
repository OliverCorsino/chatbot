import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { mustMatch } from '../../functions/passwordMatchValidator';
import { SignUpRequest } from './../../models/sign-up-request';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  /**
   * This regex will enforce these rules:
   * At least one upper case English letter, (?=.*?[A-Z])
   * At least one lower case English letter, (?=.*?[a-z])
   * At least one digit, (?=.*?[0-9])
   * At least one special character, (?=.*?[#?!@$%^&*-])
   * Minimum eight in length .{8,} (with the anchors)
   *
   * @private
   * @memberof SignUpComponent
   */
  private readonly passwordRegex = '^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$';

  signUpForm!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.buildForm();
  }

  signUp(): void {
    const signUpRequest = this.signUpForm.getRawValue() as SignUpRequest;

    console.log(signUpRequest);

    this.authService.signUp(signUpRequest).subscribe((result) => {
      this.navigateToLogin();
    }, (error) => {
      console.log(error);
    });
  }

  navigateToLogin(): void {
    this.router.navigate(['']);
  }

  private buildForm(): void {
    this.signUpForm = this.formBuilder.group({
      username: new FormControl('', [Validators.required, Validators.maxLength(30)]),
      password: new FormControl('', [Validators.required, Validators.maxLength(30), Validators.pattern(this.passwordRegex)]),
      confirmPassword: new FormControl('', [Validators.required, Validators.maxLength(30), Validators.pattern(this.passwordRegex)]),
    }, {
      validator: mustMatch('password', 'confirmPassword')
    });
  }

}
