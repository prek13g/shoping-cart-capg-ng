import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PyamentService {
  private apiUrl = 'http://localhost:5181/api/payment';
  private apiurlslist="http://localhost:5181/api/payment/batch";

    // Example API URL

  constructor(private http: HttpClient) { }
  postcart_order(id:number){
    const url = `${this.apiUrl}/${id}`; 
    return this.http.get<any>(url);
  }

   postCartOrders(cartIds: number[]): Observable<any> {
    console.log("service#####################################")
    console.log(cartIds);
    console.log("came to service");
    //console.log(cartIds)
    //const idsParam=cartIds.join(",");
    const idsParam = cartIds.map(id => `ids=${id}`).join('&');
    console.log("########################")
    console.log(idsParam)
   
   // const url = `${this.apiurlslist}?ids=${idsParam}`;
   const url = `${this.apiurlslist}?${idsParam}`;
    console.log(url);
    return this.http.get<any[]>(url);
   }


  //  postCartOrders(cartIds: number[]): Observable<any> {
  //   const url = `${this.apiUrl}/order`; // Adjust the endpoint URL if needed
  //   return this.http.post<any>(url, { cartIds });
  // }
  

  addProductToOrder(userId: number, productId: number, productQuantity: number): Observable<any> {
    const url = `http://localhost:5181/api/Payment/${userId}/add-product/${productId}`;
    return this.http.post<any>(url, productQuantity);
  }



  

}
