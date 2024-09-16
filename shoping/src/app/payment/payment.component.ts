import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PyamentService } from '../pyament.service';
import { CartService } from '../cart.service';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
 // styleUrl: './payment.component.css'
 styleUrls: ['./payment.component.css']  // Corrected property name
})
export class PaymentComponent {
  carts: any[] = [];
  id:number|null=null;
  userid:number|null=null;
  constructor(
    private route: ActivatedRoute,
    private pay:PyamentService,
    private cartService: CartService,
    private router: Router,
    // Inject the quantity service
  ) {}
  ngOnInit(): void {
    this.id = +this.route.snapshot.paramMap.get('id')!;
    const userIdString = localStorage.getItem('userId');
  console.log(userIdString);
  if (userIdString) {
    this.userid = +userIdString; // Convert to number
   
  }
    if(this.id!=null && this.id!=0){
      this.sending_cart_to_order(this.id);
      console.log("if working");
      console.log(this.id)
    }
    else if(this.userid && this.id==null || this.id==0){
      console.log("else if working");
      this.total_carts_to_payment();
    }
    console.log(this.id);

    
  }
  sending_cart_to_order(id: number | null):void{
    if (id !== null) {
      this.pay.postcart_order(id).subscribe(
        response => {
          console.log('Order processed successfully', response);
          // Navigate or update the view as needed
          
        },
        error => {
          console.error('Error processing order', error);
        }
      );
    }
           
  }


  go_order(){
    this.router.navigate(['order']);
  }




  total_carts_to_payment(){
    console.log(" came into total_carts_to_payment()")
    this.cartService.present_user_getcart(this.userid!).subscribe(
      (data) => {
        this.carts = data;
        console.log("Cart data:", this.carts);
        this.loadcartids(); 
        //console.log("Cart data:", this.carts);
        //console.log("before checking exist_cart data is there or not"+this.exist_cart)
      },(error) => {
        console.error('Error fetching data:', error);
      })

  }
// postCartOrders
  loadcartids(){
    console.log("came into loadcartids()")
    const cartIds = Array.from(new Set(this.carts.map(cartItem => cartItem.cart_id)));
    console.log("##############################################")
    console.log(cartIds);
    if (cartIds.length > 0) {
      console.log(cartIds.length)
      console.log(cartIds.length)
      console.log("came into if");

      this.pay.postCartOrders(cartIds).subscribe(
        (response) =>{
          console.log('Orders placed successfully', response);

        }, (error) => {
          console.error('Error fetching cart_ids details:', error);
        }
      );
    }
  }



}
