import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUlr = 'http://localhost:49883/api/auth/';
constructor(private http: HttpClient) { }
login(model: any) {
return this.http.post(this.baseUlr + 'login', model, )
.pipe(
  map((response: any) => {
    const user = response;
    if (user) {
      localStorage.setItem('token', user.token);
    }
  })
);
}
register(model: any) {
  return this.http.post(this.baseUlr + 'register', model);
}
}
