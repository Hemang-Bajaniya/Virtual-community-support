import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { ApiResponse, User } from '../../models/user.model';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { FormsModule } from '@angular/forms';

export interface FilterModel {
    pageSize: number;
    pageNumber: number;
    searchString: number;
    sortDirection: string;
    sortBy: string;
}

export interface PaginatedResponse<T> {
    message: string,
    data: {
        users: T[];
        totalRecords: number;
        pageNumber: number;
        pageSize: number;
        totalPages: number;
    }
}

@Injectable({
    providedIn: 'root'
})
export class AdminService {
    private baseUrl: string = 'http://localhost:5003/api/User';

    constructor(private http: HttpClient, private router: Router) { }

    // get all usrr (depre)
    getAllUsers() {
        return this.http.get<ApiResponse<User[]>>(`${this.baseUrl.replace('User', "getAllUsers")}`, { withCredentials: true });
    }

    //filter users
    getFilteredUsers(filter: FilterModel): Observable<PaginatedResponse<User>> {
        return this.http.post<PaginatedResponse<User>>(`${this.baseUrl.replace("User", "getAllUsersFileters")}`, filter, { withCredentials: true });
    }

    addUser(user: any): Observable<ApiResponse<any>> {
        return this.http.post<ApiResponse<any>>(`${this.baseUrl}/add`, user, { withCredentials: true })
    }

    updateUser(id: number, user: any): Observable<ApiResponse<any>> {
        return this.http.patch<ApiResponse<any>>(`${this.baseUrl}/${id}`, user, { withCredentials: true })
    }

    deleteUser(id: number): Observable<ApiResponse<any>> {
        return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/${id}`, { withCredentials: true })
    }
}
