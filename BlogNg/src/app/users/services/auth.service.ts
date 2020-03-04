import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../../users/models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = environment.apiUrl + 'users';

  constructor(private http: HttpClient, private router: Router) { }

  get isAuthenticated() {
    return !!localStorage.getItem('token');
  }

  getUser(): Observable<User> {
    return this.http.get<User>(`${this.url}/userdata`);
  }

  register(credentials) {
    this.http.post<any>(`${this.url}/register`, credentials).subscribe(x => {
      this.authenticate(x);
    });

    this.navigateBack();
  }

  login(credentials) {
    this.http.post<any>(`${this.url}/login`, credentials).subscribe(x => {
      this.authenticate(x);
    });

    this.navigateBack();
  }

  logout() {
    localStorage.removeItem('token');
  }

  authenticate(res) {
    localStorage.setItem('token', JSON.stringify(res));
  }

  navigateBack() {
    this.router.navigate(['/articles']);
  }
}
