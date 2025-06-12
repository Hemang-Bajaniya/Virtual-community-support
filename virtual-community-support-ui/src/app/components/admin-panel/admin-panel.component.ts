import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-admin-panel',
  standalone: true,
  imports: [NgFor, NgIf],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.css'
})
export class AdminPanelComponent implements OnInit {
  users: any = []
  error: any = ''
  loading: boolean = true

  constructor(private http: HttpClient, private service: AdminService) { }

  ngOnInit(): void {
    this.service.getAllUsers().subscribe({
      next: (res: { data: any; }) => {
        this.users = res.data;
        this.loading = false;
      },
      error: (err: any) => {
        console.log(err);
        this.error = 'Failed to fetch users.';
        this.loading = false;
      }
    });
  }
}
