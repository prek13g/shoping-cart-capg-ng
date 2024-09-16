import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5181/api/Users'; 
  private postUrl="http://localhost:5181/api/Users/register";

  constructor(private http: HttpClient) { }
  fetchdata(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }
  deletedata(user_id: any): Observable<void> {  // Ensure the method returns void or the appropriate type
    const url = `${this.apiUrl}/${user_id}`;  // Assuming API uses this format for deleting
    return this.http.delete<void>(url,{responseType:'text' as 'json'});
  }
  postuser(body:any){
    return this.http.post(this.postUrl,body)
  }
  putuser(id:number,body:any){
    return this.http.put(`${this.apiUrl}/${id}`,body,{responseType:'text' as 'json'})
  }

  getUserById(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${userId}`);
  }

}
