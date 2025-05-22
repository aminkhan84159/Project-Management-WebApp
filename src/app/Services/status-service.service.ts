import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IResult } from '../Models/userLoginData.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatusServiceService {

  constructor() { }

  mainApi="http://localhost:5194/api";
  http = inject(HttpClient)

  public getStatusTypeById(id:number) : Observable<IResult>{
    return this.http.get<IResult>(`${this.mainApi}/Status/GetStatusById?statusId=${id}`)
  }
}
