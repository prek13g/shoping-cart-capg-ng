import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5181/api/Products';  // Example API URL

  constructor(private http: HttpClient) { }
  fetchdata(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  deletedata(product_id: any): Observable<void> {  // Ensure the method returns void or the appropriate type
    const url = `${this.apiUrl}/${product_id}`;  // Assuming API uses this format for deleting
    return this.http.delete<void>(url,{responseType:'text' as 'json'});
  }
  postproduct(body:any){
    return this.http.post(this.apiUrl,body)
  }
  putproduct(id:number,body:any){
    return this.http.put(`${this.apiUrl}/${id}`,body,{responseType:'text' as 'json'})
  }
  getproduct(id:number){
    const url = `${this.apiUrl}/${id}`; 
    return this.http.get<any>(url);
  }


 // Get a single product by ID
//  getProductById(id: number): Observable<any> {
//   const url = `${this.apiUrl}/${id}`;
//   return this.http.get<any>(url);
// }

// Get multiple products by IDs
getproductsbyids(ids: number[]): Observable<any[]> {
  const idsParam = ids.join(',');
  const url = `${this.apiUrl}?ids=${idsParam}`;
  return this.http.get<any[]>(url);
}


 // Search for products based on a query
 searchProducts(query: string): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/search/${query}`);
}

addProductToOrder(userId: number, productId: number, quantity: number): Observable<any> {
  const url = `http://localhost:5181/api/Users/${userId}/add-product/${productId}`;
  return this.http.post(url, quantity);
}

}
