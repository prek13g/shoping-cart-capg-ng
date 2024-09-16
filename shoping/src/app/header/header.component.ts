import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  role: string | null = null;

  constructor( private route: ActivatedRoute, private router: Router){

  }
  ngOnInit(): void {
    // Retrieve the role from local storage on component initialization
    this.role = localStorage.getItem('role');
    this.isAdmin()
    this. isUser()
    this.isLoggedIn()
  }

  isAdmin(): boolean {
    return this.role === 'Admin';
  }

  isUser(): boolean {
    return this.role === 'User';
  }

  isLoggedIn(): boolean {
    return this.role !== null;
  }

  category_function(){
    this.router.navigate(['Category']);
  }
  product_function(){
    this.router.navigate(['Product']);
  }
  user_function(){
    this.router.navigate(['User']);
  }
  signup_function(){
    this.router.navigate(['signup']);
  }
  sign_function(){
    this.router.navigate(['signin']);
  }
  cart_function(){
    this.router.navigate(['cart']);
  }
  order_function(){
    this.router.navigate(['order']);
  }
  logout_function(){
    this.router.navigate(['logout']);
  }
  adminlogout_function(){
    this.router.navigate(['admin/logout']);
  }


  

}
