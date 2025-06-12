import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EventType, Router } from '@angular/router';
import { User } from '../../../models/user.model';
import { AdminService, FilterModel, PaginatedResponse } from '../../services/admin.service';

@Component({
  selector: 'app-user-table',
  standalone: true,
  imports: [NgFor, NgIf, ReactiveFormsModule],
  templateUrl: './user-table.component.html',
  styleUrl: './user-table.component.css'
})
export class UserTableComponent implements OnInit {
  users: User[] = [];
  totalRecords = 0;
  pageNumber = 1;
  pageSize = 4;
  totalPages = 0;
  loading = false;
  error: string | null = null;
  filterForm: FormGroup;

  constructor(private service: AdminService, private router: Router, private fb: FormBuilder) {
    this.filterForm = this.fb.group({
      searchString: [''],
      sortBy: ['Firstname'],
      sortDirection: ['Asc']
    });
  }

  ngOnInit(): void {
    this.loadUsers()
  }

  loadUsers() {
    this.loading = true
    const filter: FilterModel = {
      pageSize: this.pageSize,
      pageNumber: this.pageNumber,
      searchString: this.filterForm.get('searchString')?.value || '',
      sortBy: this.filterForm.get('sortBy')?.value || 'Firstname',
      sortDirection: this.filterForm.get('sortDirection')?.value || 'Asc'
    };

    this.service.getFilteredUsers(filter).subscribe({
      next: (response: PaginatedResponse<User>) => {

        console.log(response);

        this.users = response.data.users; // Assign only the array of users
        this.totalRecords = response.data.totalRecords;
        this.pageNumber = response.data.pageNumber;
        this.pageSize = response.data.pageSize;
        this.totalPages = response.data.totalPages;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.pageNumber = 1;
    this.loadUsers()
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.pageNumber = page;
      this.loadUsers();
    }
  }

  onDelete(userId: number) {
    if (confirm('Are you sure you want to delete this item?')) {
      this.service.deleteUser(userId).subscribe(() => {
        this.loadUsers()
      })
    }
  }

  onEdit(userId: number) {
    this.router.navigate(['/edit-user', userId]);
  }

  onAdd() {
    this.router.navigate(['/add-user']);
  }
}

