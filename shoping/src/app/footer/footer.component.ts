import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css'
})
export class FooterComponent {
  constructor(private router: Router) {}

  navigateToAdmin() {
    // This will navigate to the 'admin' route when called
    this.router.navigate(['admin']);
  }
}
