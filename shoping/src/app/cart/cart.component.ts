import { Component, OnInit } from '@angular/core';
import { CartService } from '../cart.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../product.service';
import { QuantityService } from '../quantity.service';
//import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  carts: any[] = [];
  exist_cart:any=null;
  userId: number | null = null;
  quantity: number | null = null;
  id: number | null = null;
  total_amount:any=null;
  isLoggedIn: boolean = true;


  constructor(
    private route: ActivatedRoute,
    private cartService: CartService,
    private router: Router,
    private productservice:ProductService,
    private quantityService: QuantityService // Inject the quantity service
  ) {}

  ngOnInit(): void {
    
    this.id = +this.route.snapshot.paramMap.get('id')!;
    const userIdString = localStorage.getItem('userId');
    if (userIdString) {
      this.userId = +userIdString; // Convert to number
    }else {
      this.isLoggedIn = false;
    }

    this.quantityService.quantity$.subscribe(quantity => {
      this.quantity = quantity;
      console.log('Quantity from service:', this.quantity);
    });
   
 if (this.userId) {
      this.getcarts(this.userId); // Call getcarts with userId
    } else {
      console.error('No userId found in localStorage');
    }  

    // this.quantityService.quantity$.subscribe(quantity => {
    //   this.quantity = quantity;
    //   console.log('##############################################:', this.quantity);
      
    //   // Call postcarts only if all conditions are met
    //   if (this.userId != null && this.id != null && this.quantity != null) {
    //     this.postcarts();
    //   }
    // });
   
    
    
    
    // if(this.userId!=null&&this.id!=null&&this.quantity!=null){
    //   this.postcarts();
    // }

    
    // if (this.userId) {
    //   this.getcarts(this.userId); // Call getcarts with userId
    // } else {
    //   console.error('No userId found in localStorage');
    // }  
   }

  getcarts(userId: number): void {
    this.cartService.present_user_getcart(userId).subscribe(
      (data) => {
        this.carts = data;
        this.loadProductDetails(); 
       // this.calculateTotalAmount();
      
        console.log("checking in getcart cart data is there or not"+this.carts);
        console.log("before checking exist_cart data is there or not"+this.exist_cart)

        //this.loadProductDetails();
       //this.exist_cart = this.carts.find(cartItem => cartItem.user_id === this.userId && cartItem.product_id === this.id);
        //if (this.exist_cart) {
         // this.exist_cart_func(); // Update quantity if product already exists
       // } else if (this.userId !== null && this.id !== null && this.quantity !== null) {
         // this.postcarts(); // Add new product to cart
       // }
       this.exist_cart = this.carts.find(cartItem => cartItem.user_id === this.userId && cartItem.product_id === this.id) || null;

       console.log("after check data is there or not in exist_cart"+this.exist_cart)
       if(this.exist_cart!=null){
        this.exist_cart_func()
       }
       else if(this.userId!=null&&this.id!=null&&this.quantity!=null){
        
        (this.postcarts())
        
       }



      

        // this.loadProductDetails(); // Load product details after getting carts


      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }







  loadProductDetails(): void {
    console.log("in loadproductdetals");
    const productIds = Array.from(new Set(this.carts.map(cartItem => cartItem.product_id)));
    console.log(productIds) // Get unique product IDs
  console.log(productIds.length);
    if (productIds.length > 0) {
      this.productservice.getproductsbyids(productIds).subscribe(
        (products) => {
          // Map products to cart items
          this.carts = this.carts.map(cartItem => {
            const product = products.find(p => p.product_id === cartItem.product_id);
            return { ...cartItem, product };
          });
          console.log("i am calling in loadproduct details");
          this.calculateTotalAmount();
        },
        (error) => {
          console.error('Error fetching product details:', error);
        }
      );
    }
  }









  
  increase_quantity(cartItem:any):void{
    let newQuantity = cartItem.quantity;
    newQuantity+=1;
    const body = {
          cart_id:cartItem.cart_id,
          user_id: cartItem.user_id,
          product_id: cartItem.product_id,
          quantity: newQuantity
        };

        this.cartService.putcart(cartItem.cart_id, body).subscribe({
              next: (res) => {
                console.log('Cart quantity updated successfully:', res);
                //this.getcarts(this.userId!); // Call getcarts with userId
                this.refreshCartAndProductDetails();
              },
              error: (err) => {
                console.error('Error updating cart quantity:', err);
              }
            });
          
  }
  postcarts(): void {
    // Ensure userId, id, and quantity are not null before using
   console.log("in postcarts method i am checking exist_cart:"+this.exist_cart)
//if(this.exist_cart==null){
    if (this.userId !== null && this.id !== null && this.quantity !== null) {
      const body = {
        user_id: this.userId!,
        product_id: this.id!,
        quantity: this.quantity!
      };
  
      this.cartService.postcart(body).subscribe({
        next: (res) => {
          console.log('Cart added successfully:', res);
          // Refresh cart items
          //this.refreshCartAndProductDetails();
         // this.getcarts(this.userId!); // Call getcarts with userId
         this.refreshCartAndProductDetails();
          
        },
        error: (err) => {
          console.error('Error adding to cart:', err);
        }
      });
    } else {
      console.error('Missing userId, productId, or quantity.');
    }
//}
// else{
//   this.exist_cart_func();
// }

  }

  exist_cart_func():void{

    let newquantity = this.exist_cart.quantity;
    newquantity+=this.quantity;
    const body = {
      cart_id:this.exist_cart.cart_id,
      user_id: this.exist_cart.user_id,
      product_id: this.exist_cart.product_id,
      quantity: newquantity
    };

    this.cartService.putcart(this.exist_cart.cart_id, body).subscribe({
      next: (res) => {
        console.log('Cart quantity updated successfully:', res);
        //this.getcarts(this.userId!); // Call getcarts with userId
        this.refreshCartAndProductDetails();

       


      },
      error: (err) => {
        console.error('Error updating cart quantity:', err);
      }
    });

  }





  decrease_quantity(cartItem:any):void{
    let newQuantity = cartItem.quantity;
    newQuantity-=1;
    if(newQuantity>=1){
      const body = {
        cart_id:cartItem.cart_id,
        user_id: cartItem.user_id,
        product_id: cartItem.product_id,
        quantity: newQuantity
      };
      this.cartService.putcart(cartItem.cart_id, body).subscribe({
        next: (res) => {
          console.log('Cart quantity updated successfully:', res);
          //this.getcarts(this.userId!); // Call getcarts with userId
          this.refreshCartAndProductDetails();
        },
        error: (err) => {
          console.error('Error updating cart quantity:', err);
        }
      });
    }

  }



  delete_cart(cart_id:any):void{
   // console.log(cart_id)
    this.cartService.deletecart(cart_id).subscribe(
      response => {
        // Refresh the product list after deletion\
        console.log('Delete Response:',response)
       // this.getcarts(this.userId!);
       this.refreshCartAndProductDetails();
      },
      (error) => {
        console.error('Error deleting product:', error);
      }
    );


  }



   refreshCartAndProductDetails(): void {
    if (this.userId !== null) {
      // Refresh cart items
      this.cartService.present_user_getcart(this.userId).subscribe({
        next: (data) => {
          this.carts = data;
          this.loadProductDetails();
        },
        error: (error) => {
          console.error('Error fetching updated cart data:', error);
        }
      });
    } else {
      console.error('No userId found to refresh cart.');
    }
  }
// postcarts(): void {
  //   // Ensure userId, id, and quantity are not null before using
  //   if (this.userId !== null && this.id !== null && this.quantity !== null) {
  //     const body = {
  //       user_id: this.userId!,
  //       product_id: this.id!,
  //       quantity: this.quantity!
  //     };
  
  //     this.cartService.postcart(body).subscribe({
  //       next: (res) => {
  //         console.log('Cart added successfully:', res);
  //        //this.getcarts(this.userId!); // Refresh cart items
  //        this.cartService.present_user_getcart(this.userId).subscribe(
  //         (data) => {
  //           this.carts = data;
            
  //           this.loadProductDetails();
  //                },
  //            );
  //       },
  //       error: (err) => {
  //         console.error('Error adding to cart:', err);
  //       }
  //     });
  //   } else {
  //     console.error('Missing userId, productId, or quantity.');
  //   }
  // }







//   calculateTotalAmount(): void {
//     let total = 0;
// console.log("total amount function working")
//     this.carts.forEach(function(item) {
//       total += item.product.product_Price * item.quantity;
//     });

//     this.total_amount = total;
//     console.log(this.total_amount);
//   }


calculateTotalAmount(): void {
  console.log("came into tota1mamount function")
  let total = 0;
  this.carts.forEach(function(cartItem) {
console.log("above if");
console.log(cartItem.product);
console.log(cartItem.product.product_Price);
    if (cartItem.product && cartItem.product.product_Price) {
      console.log("came into inside if");
      total += cartItem.product.product_Price * cartItem.quantity;
    }
  });
  this.total_amount = total;
  console.log(this.total_amount);
}






// single_cart_sending_order(id: number):void{
//   // this.productService.getproduct(id).subscribe({
//   //   next: (data) => this.product = data,
//   //   error: (err) => console.error(err)
//   //});

// }

sending_into_payment(id:number){
  this.router.navigate(['payment',id]);
}



total_cart_sending_to_payment(){
  this.router.navigate(['payment']);
}





signin_page(){
  this.router.navigate(['signin']);
}



}

