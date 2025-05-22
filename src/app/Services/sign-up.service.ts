import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { userSignupData } from '../Models/userSignUpData.model';
import { userDetails } from '../Models/userDetails.model';
import { IResult } from '../Models/userLoginData.model';

@Injectable({
  providedIn: 'root'
})
export class SignUpService {

  mainApi="http://localhost:5194/api"
  private http=inject(HttpClient);
  constructor() { }

  public addSignupUser(obj:userSignupData):Observable<IResult>{
    return this.http.post<IResult>(this.mainApi + "/User/AddUser",obj);
  }

  public getSignupUserDataByUserid(id:number):Observable<IResult>{
    return this.http.get<IResult>(`http://localhost:5194/api/User/GetUserById?userId=${id}`,
      {}
    )
  }

  public addRegisterUser(obj:userDetails):Observable<IResult>{
    return this.http.post<IResult>(this.mainApi + "/UserDetail/AddUserDetails",obj);
  }

  setToken(token:string){
    localStorage.setItem('token',token);
    this.isLoggedIn.next(true);
  }
  getToken(): string | null {
    if (typeof window !== 'undefined' && localStorage) {
      return localStorage.getItem('token');
      this.isLoggedIn.next(true);
    }
    return null;
  }

  removeToken(){
    localStorage.setItem('token',"");
    this.isLoggedIn.next(false);
  }

  public isLoggedIn = new BehaviorSubject<boolean>(false);

    get isLoggedIn$(): Observable<boolean>{
      return this.isLoggedIn.asObservable();
    }
}
