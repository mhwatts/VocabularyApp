import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
<<<<<<< HEAD
=======
import { PasswordInputComponent } from '../password-input/password-input.component';
>>>>>>> fork/feat/remove-userword-fields
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
<<<<<<< HEAD
  imports: [CommonModule, ReactiveFormsModule],
=======
  imports: [CommonModule, ReactiveFormsModule, PasswordInputComponent],
>>>>>>> fork/feat/remove-userword-fields
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isLoading = false;
  errorMessage = '';
  successMessage = '';
<<<<<<< HEAD
=======
  passwordVisible = false;
>>>>>>> fork/feat/remove-userword-fields

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    // Check for success message from registration
    this.route.queryParams.subscribe(params => {
      if (params['message']) {
        this.successMessage = params['message'];
      }
    });

    // Redirect if already authenticated
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/dashboard']);
    }
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.authService.login(this.loginForm.value).subscribe({
        next: (response) => {
          this.isLoading = false;
          if (response.success) {
            // Login successful, navigate to dashboard
            this.router.navigate(['/dashboard']);
          } else {
            this.errorMessage = response.message || 'Login failed';
          }
        },
        error: (error) => {
          this.isLoading = false;
          this.errorMessage = error.error?.message || 'An error occurred during login';
        }
      });
    }
  }

  navigateToSignup(): void {
    this.router.navigate(['/signup']);
  }
<<<<<<< HEAD
=======

  togglePasswordVisibility(): void {
    this.passwordVisible = !this.passwordVisible;
  }
>>>>>>> fork/feat/remove-userword-fields
}
