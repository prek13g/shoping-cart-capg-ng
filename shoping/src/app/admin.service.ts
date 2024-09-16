import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl="http://localhost:5181/api/Admin/Login";
  constructor(private http: HttpClient) { }

  postloginadmin(body: any): Observable<{ token: string, username: string,  useremail: string,role:string}> {
    console.log(body)
    return this.http.post<{ token: string, username: string,  useremail: string,role:string }>(this.apiUrl, body);
  }
  

}
