import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = 'http://localhost:5181/api/Categories';

  constructor(private http: HttpClient) { }
  fetchdata(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  deletedata(category_id: any): Observable<void> {  // Ensure the method returns void or the appropriate type
    const url = `${this.apiUrl}/${category_id}`;  // Assuming API uses this format for deleting
    return this.http.delete<void>(url,{responseType:'text' as 'json'});
  }
  postcategory(body:any){
    return this.http.post(this.apiUrl,body)
  }
  putcategory(id:number,body:any){
    return this.http.put(`${this.apiUrl}/${id}`,body,{responseType:'text' as 'json'})
  }
  
}
