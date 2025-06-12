import { Routes } from '@angular/router';
import { LoginComponent } from '../app/components/login/login.component';
import { RegisterComponent } from '../app/components/register/register.component';
import { ProfileComponent } from '../app/components/profile/profile.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { UserTableComponent } from './components/user-table/user-table.component';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { TestComponent } from './components/test/test.component';

export const appRoutes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: TestComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'profile', component: ProfileComponent },
    { path: 'admin', component: UserTableComponent },
    // { path: 'users', component: UserTableComponent },
    { path: 'add-user', component: UserFormComponent },
    { path: 'edit-user/:id', component: UserEditComponent },
    { path: "test", component: TestComponent }
];
