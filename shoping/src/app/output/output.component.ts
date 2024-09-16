import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-output',
  templateUrl: './output.component.html',
  styleUrl: './output.component.css'
})
export class OutputComponent {
  constructor(private router:Router){}
  ngOnInit(): void {
     this.logout_function();
  }

  logout_function(){
    localStorage.removeItem('authToken');
    localStorage.removeItem('username');

    localStorage.removeItem('userId');
    localStorage.removeItem('userEmail');
    localStorage.removeItem('role');
   
    this.router.navigate([''])
    


  }

}
