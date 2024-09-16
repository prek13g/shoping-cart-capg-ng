import { Component } from '@angular/core';
import { CategoryService } from '../category.service';


@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  categorys: any[] = [];
  formHeader = "Add Category";
  showForm = false;
  categoryName = "";
  categoryId=null;
  constructor(private category:CategoryService){

  }
  ngOnInit(): void {
    this.getcategorys();
  }
  getcategorys(): void {
    this.category.fetchdata().subscribe(
      (data) => {
        this.categorys = data;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }
  deletecategory(category_id: any): void {
    this.category.deletedata(category_id).subscribe(
      response => {
        // Refresh the product list after deletion\
        console.log('Delete Response:',response)
        this.getcategorys();
      },
      (error) => {
        console.error('Error deleting product:', error);
      }
    );
  }

  openform(data:any=null){
    
    this.showForm=true;
    if(data){
      this.categoryId = data.category_id;
      this.categoryName=data.category_name;
    this.formHeader = "Edit Category";
    }
    else{
      this.categoryId=null;
      this.formHeader="Add Category"
    }
  }
  closeform(){
    this.showForm=false;
    this.clearform()
  }
  clearform(){
    this.categoryName = "";
      
  }
  savecategory(){
    this.showForm=false;
    let body:any = {
      category_name:this.categoryName
  }
  if(this.categoryId){
    body['category_id']=this.categoryId;
    this.category.putcategory(this.categoryId,body).subscribe((res)=>{
      console.log('Category updated successfully:', res);
      this.getcategorys();
    })

  }
  else{
    this.category.postcategory(body).subscribe((res)=>{
      console.log('Category added successfully:', res);
      this.getcategorys();
    })

  }
}



}
