import { Component } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { ProductService } from '../product.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  product: any[] = [];
  formHeader = "Add Product";
  productName = "";
  productImage="";
  productPrice: number | null = null;
  productDescription = "";
  productQuantity: number | null = null;
  categoryId:number | null=null;
  showForm = false;
  productId=null;
  constructor(private prod:ProductService){

  }
  ngOnInit(): void {
    this.getproducts();
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
  deleteproduct(product_id: any): void {
    this.prod.deletedata(product_id).subscribe(
      response => {
        // Refresh the product list after deletion\
        console.log('Delete Response:',response)
        this.getproducts();
      },
      (error) => {
        console.error('Error deleting product:', error);
      }
    );
  }
  openform(data:any=null){
    
    this.showForm=true;
    if(data){
      this.productName = data.product_name;
      this.productImage=data.product_image;
    this.productPrice = data.product_Price;
    this.productDescription = data.product_description;
    this.productQuantity = data.product_Quantity;
    this.productId = data.product_id;
    this.categoryId=data.category_id;
    this.formHeader = "Edit Product";
    }
    else{
      this.productId=null;
      this.formHeader="Add Product"
    }
  }
  closeform(){
    this.showForm=false;
    this.clearform()
  }
  clearform(){
    this.productName = "";
    this.productImage="";
       this.productPrice=null;
      this.productDescription = "";
      this.productQuantity= null;
      this.categoryId=null;
  }
  saveproduct(){
    this.showForm=false;
    let body:any = {
      product_name: this.productName,
      product_image:this.productImage,
      product_Price: this.productPrice,
      product_description: this.productDescription,
      product_Quantity: this.productQuantity,
      category_id:this.categoryId,

    }


    if(this.productId){
      body['product_id']=this.productId;
      this.prod.putproduct(this.productId,body).subscribe((res)=>{
        console.log('Product updated successfully:', res);
        this.getproducts();
      })

    

    }
    else{
      this.prod.postproduct(body).subscribe((res)=>{
        console.log('Product added successfully:', res);
        this.getproducts();
      })

    }
    
  }
 

 }


