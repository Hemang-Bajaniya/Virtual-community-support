import { Component } from '@angular/core';
// import ConfirmDeleteComponent from '../confirm-delete';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { TopbarComponent } from '../topbar/topbar.component';
import { UserTableComponent } from '../user-table/user-table.component';
import { UserFormComponent } from '../user-form/user-form.component';
import { ConfirmDeleteComponent } from '../confirm-delete/confirm-delete.component';


@Component({
  selector: 'app-user-managment',
  standalone: true,
  imports: [CommonModule,
    SidebarComponent,
    TopbarComponent,
    UserTableComponent,
    UserFormComponent,
    ConfirmDeleteComponent],
  templateUrl: './user-managment.component.html',
  styleUrl: './user-managment.component.css'
})
export class UserManagmentComponent {
  users: any;
  showForm() {
    throw new Error('Method not implemented.');
  }
  edit(arg0: any) {
    throw new Error('Method not implemented.');
  }
  user: any;
  confirmDelete(arg0: any) {
    throw new Error('Method not implemented.');
  }
  selectedUser: any;
  hideForm() {
    throw new Error('Method not implemented.');
  }
  delete() {
    throw new Error('Method not implemented.');
  }
  cancelDelete() {
    throw new Error('Method not implemented.');
  }

}
