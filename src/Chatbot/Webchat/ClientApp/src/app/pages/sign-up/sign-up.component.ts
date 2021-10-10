import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { mustMatch } from '../../functions/passwordMatchValidator';

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
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.buildForm();
  }

  signUp(): void {

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
