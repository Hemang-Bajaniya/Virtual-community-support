import { NgIf } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Route, Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, RouterLink],
  templateUrl: './test.component.html',
  styleUrl: './test.component.css'
})
export class TestComponent {
  loginForm: FormGroup;
  formValid: boolean = false;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private service: AuthService
  ) {
    this.loginForm = this.fb.group({
      emailAddress: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  get emailAddress() {
    return this.loginForm.get('emailAddress')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  onSubmit() {
    console.log("ok");

    this.formValid = true;

    if (this.loginForm.invalid) {
      return;
    }

    const payload = {
      email: this.loginForm.value.emailAddress,
      password: this.loginForm.value.password
    };

    this.service.login(payload).subscribe({
      next: () => { },
      error: (err) => {
        console.log(err);
        alert('Invalid credentials')
      }
    });
  }
}
