import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 @Output() cancelRegister = new EventEmitter();
  model: any = {};
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;
  constructor(private authService: AuthService, private alertity: AlertifyService, private fb: FormBuilder) { }


  ngOnInit() {
   /*  this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]),
      confimPassword: new FormControl('', Validators.required)
    },this.passwordMathValidator); */

    this.bsConfig ={
      containerClass: 'theme-red'
    }
    this.createRegisterForm();
  }
  passwordMathValidator(g: FormGroup) {
    return g.get('password').value === g.get('confimPassword').value ? null : { 'mismatch': true};
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: [ null , Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', Validators.required],
      confimPassword: ['', Validators.required],
    }, {validators: this.passwordMathValidator} );
    }

  register() {
    // this.authService.register(this.model).subscribe(() => {
    //   this.alertity.success('registration Successful');
    // }, error => {
    //  this.alertity.error(error);
    // });
    console.log(this.registerForm.value);
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
