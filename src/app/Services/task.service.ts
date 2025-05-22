import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IResult } from '../Models/userLoginData.model';
import { Observable } from 'rxjs';
import { ITask } from '../Models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor() { }

  mainApi="http://localhost:5194/api";
  http = inject(HttpClient)

  public getTaskByProjectId(id: number) : Observable<IResult>{
    return this.http.get<IResult>(`${this.mainApi}/Task/GetTasksByProjectId?projectId=${id}`)
  }

  public createTask(obj: ITask): Observable<IResult>{
    return this.http.post<IResult>(`${this.mainApi}/Task/AddTask`,obj)
  }
}
