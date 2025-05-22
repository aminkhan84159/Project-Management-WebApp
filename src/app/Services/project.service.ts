import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { IProject } from '../Models/Project.model';
import { IResult } from '../Models/userLoginData.model';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  mainApi="http://localhost:5194/api"
  private http=inject(HttpClient);
  
  constructor() { }

   public createProject(obj:IProject):Observable<IResult>{
      return this.http.post<IResult>(this.mainApi + "/Project/AddProject",obj);
    }

    public getProjectByUserId(id:any):Observable<IResult>{
      return this.http.get<IResult>(`${this.mainApi}/Relation/GetProjectsByUserId?userId=${id}`,
        {}
      )
    }

    public getMemberCountByProjectId(id:any):Observable<IResult>{
      return this.http.get<IResult>(`${this.mainApi}/Relation/GetMembersCountByProjectId?projectId=${id}`,
      {}
    )
    }

    public getTaskCountByProjectId(id:any):Observable<IResult>{
      return this.http.get<IResult>(`${this.mainApi}/Task/GetTasksCountByProjectId?projectId=${id}`,
        {}
      )
    }

    public getProjectById(id:any): Observable<IResult>{
      return this.http.get<IResult>(`${this.mainApi}/Project/GetProjectById?projectId=${id}`)
    }

    public projectId = new BehaviorSubject<number>(0);

    get projectId$(): Observable<number>{
      return this.projectId.asObservable();
    }
}
