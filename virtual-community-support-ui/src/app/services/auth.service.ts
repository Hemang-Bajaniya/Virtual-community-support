import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl: string = 'http://localhost:5003/api/User';
  private userInfo: any;

  constructor(private http: HttpClient, private router: Router) { }

  register(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/register`, data);
  }

  login(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/login`, data, { withCredentials: true }).pipe(
      tap(() => {
        this.fetchUserInfo().subscribe(user => {
          this.userInfo = user.Data;

          // console.log(user);

          if (user.data.role === "Admin") {
            this.router.navigate(['/admin']);
          } else {
            this.router.navigate(['/profile']);
          }
        });
      })
    );
  }

  fetchUserInfo(): Observable<any> {
    return this.http.get(`${this.baseUrl}/me`, { withCredentials: true });
  }

  isAdmin(): boolean {
    return this.userInfo && this.userInfo.role === "Admin";
  }

  getUserProfile(): Observable<any> {
    return this.http.get(`${this.baseUrl}/profile`, { withCredentials: true });
  }

  logout(): void {
    document.cookie = '';
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    const match = document.cookie.match(new RegExp('(^| )token=([^;]+)'));
    return match ? match[2] : null;
  }
}
