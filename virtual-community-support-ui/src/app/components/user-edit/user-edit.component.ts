import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AdminService } from '../../services/admin.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  standalone: true,
  styleUrl: './user-edit.component.css',
  imports: [ReactiveFormsModule],
})
export class UserEditComponent implements OnInit {
  form: FormGroup;
  userId!: number;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: AdminService,
    private router: Router
  ) {
    this.form = this.fb.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      phonenumber: ['', Validators.required],
      emailaddress: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.userId = +this.route.snapshot.paramMap.get('id')!;
    this.userService.getAllUsers().subscribe(res => {
      const user = res.data.find(u => u.id === this.userId);
      // console.log(user);

      if (user) {
        this.form.patchValue(user);
      }
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.userService.updateUser(this.userId, this.form.value).subscribe(() => {
        this.router.navigate(['/admin']);
      });
    }
  }
}
