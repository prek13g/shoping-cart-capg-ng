import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-adminlogout',
  templateUrl: './adminlogout.component.html',
  styleUrl: './adminlogout.component.css'
})
export class AdminlogoutComponent {

constructor(private router:Router){}

ngOnInit(): void {
  this.admin_logout_function();
}


  admin_logout_function(){
    localStorage.removeItem('authToken');
    localStorage.removeItem('username');

    localStorage.removeItem('userid');
    localStorage.removeItem('useremail');
    localStorage.removeItem('role');
   
    //localStorage.removeItem('username');
    this.router.navigate([''])
    


  }


}
