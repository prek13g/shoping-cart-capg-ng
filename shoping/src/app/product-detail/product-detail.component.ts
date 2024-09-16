import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../product.service';
import { QuantityService } from '../quantity.service';
import { UserService } from '../user.service';
import { PyamentService } from '../pyament.service';


@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent {
 product:any;
 quantity:number=1;
 userId: number = +localStorage.getItem('userId')!; // Get user ID from localStorage

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
     private router: Router, // Import Router
     private quantityService: QuantityService, // Inject the quantity service
     private userService: UserService,
     private paymentService: PyamentService
  ) { }
  ngOnInit(): void {
    // Get the product ID from the route parameters
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.getProduct(id);
  }
  getProduct(id: number): void {
    this.productService.getproduct(id).subscribe({
      next: (data) => this.product = data,
      error: (err) => console.error(err)
    });
  }

  

  addToCart(productId: number): void {
     // Set quantity in QuantityService
     this.quantityService.setQuantity(this.quantity);
    // Example implementation: redirect to cart page
    this.router.navigate(['cart',productId]);
    // You can also pass data to the cart component if needed
  }


  // buyNow(productId: number): void {
  //   // Assuming user ID is known or can be retrieved from a service
  //   this.userService.getUserById(this.userId).subscribe(user => {
  //     if (user) {
  //       // Create order request
  //       const order = {
  //         productId: productId,
  //         productQuantity: this.quantity
  //       };

  //       this.productService.addProductToOrder(this.userId, productId, this.quantity).subscribe({
  //         next: () => {
  //           alert('Order placed successfully');
  //           this.router.navigate(['/orders']);
  //         },
  //         error: (err) => console.error('Error placing order', err)
  //       });
  //     } else {
  //       alert('User not found');
  //     }
  //   });
  // }
  
  buyNow(userId: number, productId: number, productQuantity: number): void {
    this.paymentService.addProductToOrder(userId, productId, productQuantity).subscribe({
      next: (response) => {
        console.log('Order placed successfully!', response);
        alert("Order placed successfully");
      },
      error: (err) => console.error('Error placing order:', err),
      
    });
  }
  

  increaseQuantity(): void {
    if (this.quantity < this.product.product_Quantity) { // Ensure it doesn't exceed available quantity
      this.quantity++;
    }
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) { // Ensure it doesn't go below 1
      this.quantity--;
    }
  }



}
