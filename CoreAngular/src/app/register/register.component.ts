import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

 @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService, private alertity: AlertifyService) { }

  ngOnInit() {
  }
  register() {
    this.authService.register(this.model).subscribe(() => {
      this.alertity.success('registration Successful');
    }, error => {
     this.alertity.error(error);
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
