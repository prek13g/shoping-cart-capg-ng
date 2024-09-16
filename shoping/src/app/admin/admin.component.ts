import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AdminService } from '../admin.service';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
 styleUrl: './admin.component.css'
// styleUrls: ['./signin.component.css'] // Make sure to use 'styleUrls' instead of 'styleUrl'
 
})
export class AdminComponent {
constructor(private adminservice:AdminService,private router:Router){}



onSubmit(form: NgForm) {
  if (form.valid) {
    const loginData = {
      user_email: form.value.email,
      password: form.value.password
    };
console.log("11111111111111");
console.log(loginData)
    this.adminservice.postloginadmin(loginData).subscribe(
      response => {
        console.log('Login successful', response);
        //this.router.navigate([""]);
        //const { token, username, user_id, user_email, role } = response;
        const { token, username, useremail, role} = response;
        console.log('Response properties:', { token, username,  useremail, role });


        console.log("above if");
       // Access the token from the response
        if (token && username  && useremail) {
          console.log("inside if");
         localStorage.setItem('authToken', token); // Store the token in local storage
          localStorage.setItem('username', username); // Store the username in local storage
          // localStorage.setItem('userid', userid.toString()); // Store the user ID in local storage
          localStorage.setItem('useremail', useremail);
          localStorage.setItem('role',role)

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
