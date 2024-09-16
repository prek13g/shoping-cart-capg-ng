import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class QuantityService {

  // Create a BehaviorSubject to hold the quantity state
  private quantitySubject = new BehaviorSubject<number>(1); // Default value is 1
  // Observable stream for components to subscribe to
  quantity$ = this.quantitySubject.asObservable();
  setQuantity(quantity: number): void {
    this.quantitySubject.next(quantity);
  }
}
