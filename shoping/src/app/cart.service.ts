import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5181/api/Carts';  // Example API URL

  constructor(private http: HttpClient) { }

  postcart(body: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, body);
  }

  present_user_getcart(id: number): Observable<any[]> {
    const url = `${this.apiUrl}/${id}`; 
    return this.http.get<any[]>(url);
  }

  putcart(id: number, body: any): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, body, { responseType: 'text' as 'json' });
  }

  deletecart(id: number): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`, { responseType: 'text' as 'json' });
  }
}
