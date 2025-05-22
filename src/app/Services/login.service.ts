import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserLoginData, IResult } from '../Models/userLoginData.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  mainApi="http://localhost:5194/api";
  private http=inject(HttpClient);
  constructor() { }

  public userLogin(obj:UserLoginData):Observable<IResult>{
    // return this.http.post<IResultSignup>(this.mainApi + "/Login/UserLogin", obj);
    return this.http.post<IResult>(`${this.mainApi}/Login/Email/Username?info=${obj.loginData}&password=${obj.password}`,
      {}
    )
  }

}
