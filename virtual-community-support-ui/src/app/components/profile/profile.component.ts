import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { NgIf } from '@angular/common';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  user: any
  loading: boolean = true;
  error: string = '';

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.getUserProfile().subscribe({
      next: (data) => {
        console.log(data.data);
        this.user = data.data
        this.loading = false
      },
      error: (err) => {
        console.log(err);

        this.error = err
        this.loading = false
      }
    });
  }

  logout() {
    this.authService.logout()
  }

}
