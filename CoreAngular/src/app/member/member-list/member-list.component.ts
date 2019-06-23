import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { error } from 'util';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
users: User[];
  constructor(private userService: UserService, private alertifry: AlertifyService) { }

  ngOnInit() {
    // this.loadUser();
    this.userService.getUsers().subscribe((users: User[]) => {
      this.users = users;
    // tslint:disable-next-line:no-shadowed-variable
    }, error => {
      this.alertifry.error(error);
    });
  }
 /* loadUser() {
    this.userService.getUsers().subscribe((users: User[]) => {
       this.users = users;
    }, error => {
      this.alertifry.error(error);
    });
  }
*/
}
