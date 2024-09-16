import { Component } from '@angular/core';
import { OrderService } from '../order.service';
import { ProductService } from '../product.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  //styleUrl: './order.component.css',
  styleUrls: ['./order.component.css'] // Fixed styleUrls (plural)
})
export class OrderComponent {
  orders: any[] = [];
  userId: number | null = null;
  isLoggedIn: boolean = true;
constructor(private order:OrderService,
  private productservice:ProductService,
  private router:Router
){}
ngOnInit():void{
  const userIdString = localStorage.getItem('userId');
  console.log(userIdString);
  if (userIdString) {
    this.userId = +userIdString; // Convert to number
   
  }else {
    this.isLoggedIn = false;
  }
  if (this.userId) {
    this.getorders_byuser(this.userId); 
    console.log(this.userId);// Call getcarts with userId
  } else {
    console.error('No userId found in localStorage');
  }  
}
getorders_byuser(userId: number):void{
  this.order.fetch_orders_by_user(userId).subscribe(
    (data) => {
      this.orders = data;
      this.loadProductDetails(); 
    },
    (error) => {
      console.error('Error fetching data:', error);
    }
  );

}


loadProductDetails(): void {
  const productIds = Array.from(new Set(this.orders.map(orderItem => orderItem.product_id))); // Get unique product IDs
  if (productIds.length > 0) {
    this.productservice.getproductsbyids(productIds).subscribe(
      (products) => {
        // Map products to cart items
        this.orders = this.orders.map(orderItem => {
          const product = products.find(p => p.product_id === orderItem.product_id);
          return { ...orderItem, product };
        });
      },
      (error) => {
        console.error('Error fetching product details:', error);
      }
    );
  }
}


delete_order(order_id:any):void{
  // this.prod.deletedata(product_id).subscribe(
  //   response => {
  //     // Refresh the product list after deletion\
  //     console.log('Delete Response:',response)
  //     this.getproducts();
  //   },
  //   (error) => {
  //     console.error('Error deleting product:', error);
  //   }
 // );
 console.log(order_id)
 this.order.delete_order_by_user(order_id).subscribe(
  response =>{
     // Refresh the order list after deletion\
     console.log('Delete Response:',response)
     console.log(this.userId);
     this.getorders_byuser(this.userId!);
     console.log("this is going again to get_orders function");
     
  },
  (error) => {
    //     console.error('Error deleting product:', error);
    //   }
  }
 );

}



signin_page(){
  this.router.navigate(['signin']);
}




}