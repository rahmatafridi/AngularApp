import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
jwtHelper = new JwtHelperService();
decodedToken: any;
baseUlr = 'http://localhost:49883/api/auth/';
constructor(private http: HttpClient) { }
login(model: any) {
return this.http.post(this.baseUlr + 'login', model, )
.pipe(
  map((response: any) => {
    const user = response;
    if (user) {
      localStorage.setItem('token', user.token);
      this.decodedToken = this.jwtHelper.decodeToken(user.token);
      console.log(user.decodeToken);
    }
  })
);
}
register(model: any) {
  return this.http.post(this.baseUlr + 'register', model);
}

loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}
}
