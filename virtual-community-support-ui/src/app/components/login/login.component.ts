import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true,
  imports: [FormsModule, RouterLink, NgIf]
})
export class LoginComponent {
  user = { email: '', password: '' };
  private loadingSubject =
    new BehaviorSubject<boolean>(false);
  loading = false;

  constructor(private authService: AuthService, private router: Router) { }

  loading$ = this.loadingSubject.asObservable();

  loadingOn() {
    this.loadingSubject.next(true);
  }

  loadingOff() {
    this.loadingSubject.next(false);
  }

  login() {
    this.loading = true;
    this.authService.login(this.user).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/profile']);
      },
      error: (err) => {
        this.loading = false;
        console.log(err);
        alert('Invalid credentials')
      }
    });
  }
}
