import { Component } from '@angular/core';
import { ProductService } from '../product.service';
//import { Router } from 'express';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  product: any[] = [];
  username: string | null = null;
  searchQuery: string = ''; 
  userId: string | null = null;
  userEmail: string | null = null;
  constructor(private prod:ProductService,private refresh:Router){

  }
  ngOnInit(): void {
    this.getproducts();
    this.username = localStorage.getItem('username');
    this.userId = localStorage.getItem('userId');
    this.userEmail = localStorage.getItem('userEmail');
   
  }
  getproducts(): void {
    this.prod.fetchdata().subscribe(
      (data) => {
        this.product = data;
        console.log(this.product)
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }

  // Search products based on the search query
  onSearch(): void {
    if (this.searchQuery.trim() === '') {
      this.getproducts();  // If the search query is empty, load all products
      return;
    }

    this.prod.searchProducts(this.searchQuery).subscribe(
      (filteredProducts) => {
        this.product = filteredProducts;
        if (this.product.length === 0) {
          console.log('No products found for search query:', this.searchQuery);
        }
      },
      (error) => {
        console.error('Error searching for products:', error);
        this.product = [];
      }
    );
  }

}

