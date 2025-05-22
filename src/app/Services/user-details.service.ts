import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IResult } from '../Models/userLoginData.model';

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  mainApi="http://localhost:5194/api"
  http=inject(HttpClient);
  // http://localhost:5194/api/UserDetail/UpdateUserDetailsById?userDetailId=119

  constructor() { }

  public getUserDetailById(id:any):Observable<IResult>{
    return this.http.get<IResult>(`${this.mainApi}/UserDetail/GetUserDetailsByUserId?userId=${id}`,{});
  }

  public getUserSignupDetaiById(id:any):Observable<IResult>{
    return this.http.get<IResult>(`${this.mainApi}/User/GetUserById?userId=${id}`,{});
  }

  public getUserById(id: any): Observable<IResult>{
    return this,this.http.get<IResult>(`${this.mainApi}/User/GetUserById?userId=${id}`)
  }
}
