import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserloginService {
  private apiUrl="http://localhost:5181/api/User/Login";
  constructor(private http: HttpClient) { }
  // postloginuser(body:any){
  //   return this.http.post(this.apiUrl,body)
  // }
  postloginuser(body: any): Observable<{ token: string, username: string, userId: number, userEmail: string,userRole:string}> {
    return this.http.post<{ token: string, username: string, userId: number, userEmail: string,userRole:string }>(this.apiUrl, body);
  }



  // postloginuser(body: any): Observable<{ token: string, username: string }> {
  //   return this.http.post<{ token: string, username: string }>(this.apiUrl, body);
  // }

  
}
