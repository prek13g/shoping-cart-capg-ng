import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UserloginService } from '../userlogin.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'] // Make sure to use 'styleUrls' instead of 'styleUrl'
})
export class SigninComponent {
  constructor(private userloginService: UserloginService, private router: Router) { }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const loginData = {
        user_email: form.value.email,
        password: form.value.password
      };

      this.userloginService.postloginuser(loginData).subscribe(
        response => {
          console.log('Login successful', response);
          //this.router.navigate([""]);
          //const { token, username, user_id, user_email, role } = response;
          const { token, username, userId, userEmail, userRole} = response;



         // Access the token from the response
          if (token && username && userId && userEmail) {
           localStorage.setItem('authToken', token); // Store the token in local storage
            localStorage.setItem('username', username); // Store the username in local storage
             localStorage.setItem('userId', userId.toString()); // Store the user ID in local storage
            localStorage.setItem('userEmail', userEmail);
            localStorage.setItem('userRole',userRole)
            console.log(userRole)

           // localStorage.setItem('role',role) ;// Store the user email in local storage
            // localStorage.setItem('authToken', token); // Store the token in local storage
            // localStorage.setItem('username', username); // Store the username in local storage
            // localStorage.setItem('userId', userId); // Store the user ID in local storage
            // localStorage.setItem('userEmail', useremail); // Store the username in local storage
          this.router.navigate([""]); // Redirect to a protected route, e.g., dashboard
           }
        },
        error => {
          console.error('Login failed', error);
          // Handle login failure, e.g., show an error message to the user
        }
      );
    }
  }
}
