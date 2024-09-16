import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl="http://localhost:5181/api/Orders";
  private apiurl="http://localhost:5181/api/Orders/cancellation";

  constructor(private http:HttpClient) { }
  fetch_orders_by_user(id:number):Observable<any[]>{
     const url=`${this.apiUrl}/${id}`; 
     return this.http.get<any[]>(url);
  }
  delete_order_by_user(id:any): Observable<void> {
    console.log(id)
    const url=`${this.apiurl}/${id}`; 
    console.log(url)
    return this.http.delete<void>(url,{responseType:'text' as 'json'});

  }

  // New method to get user by id
  getUserById(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${userId}`);
  }
  // present_user_getcart(id: number): Observable<any[]> {
  //   const url = `${this.apiUrl}/${id}`; 
  //   return this.http.get<any[]>(url);
  // }
}
