
import { Component } from '@angular/core';
import { UserService } from '../user.service';
import { HttpClient } from '@angular/common/http';



@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  user: any[] = [];
  showForm = false;
  userName="";
  userEmail="";
  userId=null;
  Password="";
  formHeader = "Add User";

  
  constructor(private User:UserService){

  }
  ngOnInit(): void {
    this.getusers();
  }
  getusers(): void {
    this.User.fetchdata().subscribe(
      (data) => {
        this.user = data;
      },
      (error) => {
        console.error('Error fetching data:', error);
      }
    );
  }
  

  deleteuser(user_id: any): void {
    this.User.deletedata(user_id).subscribe(
      response => {
        // Refresh the product list after deletion\
        console.log('Delete Response:',response)
        this.getusers();
      },
      (error) => {
        console.error('Error deleting product:', error);
      }
    );
  }


  openform(data:any=null){
    
    this.showForm=true;
    if(data){
      this.userId=data.user_id;
      this.userName = data.user_name;
    this.userEmail = data.user_email;
    this.Password = data.password;
    this.formHeader = "Edit User";
   
    }
    else{
      this.userId=null;
      this.formHeader="Add User"
    }
  }
  closeform(){
    this.showForm=false;
    this.clearform()
  }
  clearform(){
    this.userName = "";
       this.userEmail="";
      this.Password = "";
      
  }


  saveuser(){
    this.showForm=false;
    let body:any = {
      user_name: this.userName,
      user_email: this.userEmail,
      password: this.Password,
     

    }
    if(this.userId){
      body['user_id']=this.userId;
      this.User.putuser(this.userId,body).subscribe((res)=>{
        console.log('User updated successfully:', res);
        this.getusers();
      })

    

    }
    else{
      this.User.postuser(body).subscribe((res)=>{
        console.log('User added successfully:', res);
        this.getusers();
      })

    }
    
  }


  

  

 
  

}
