import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../user.service';
import { Router } from '@angular/router'; // Import Router

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  constructor(private userService: UserService,private router: Router) { }
  onSubmit(form: NgForm) {
    if (form.valid) {
      if (form.value.password==form.value.confirmPassword){
      const userData = {
        user_name: form.value.username,
        user_email: form.value.email,
        password: form.value.password,
        
      };
    

      this.userService.postuser(userData).subscribe(
        response => {
          console.log('Registration successful', response);

          // Optionally, redirect to a different page or show a success message
           // Redirect to login page
           this.router.navigate(['signin']);
        },
        error => {
          console.error('Registration failed', error);
          // Optionally, show an error message to the user
        }
      );
    }
    else {
      console.error('Passwords do not match');
      // Optionally, show a validation error message
    }
  }
  }

}
