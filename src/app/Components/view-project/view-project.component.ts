import { Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { IProject } from '../../Models/Project.model';
import { ProjectService } from '../../Services/project.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserDetailsService } from '../../Services/user-details.service';
import { IUser } from '../../Models/userDetails.model';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { IResult } from '../../Models/userLoginData.model';
import { StatusServiceService } from '../../Services/status-service.service';
import { IStatus } from '../../Models/status.model';

import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-view-project',
  imports: [MatCardModule,
            MatDividerModule,
            MatButtonModule,
            ],
  templateUrl: './view-project.component.html',
  styleUrl: './view-project.component.css'
})
export class ViewProjectComponent {

  projectService = inject(ProjectService)
  userdetailService = inject(UserDetailsService)
  statusService = inject(StatusServiceService)
  activatedRoute = inject(ActivatedRoute)
  router = inject(Router)
  project : IProject | any = {}
  user: IUser | any = {}
  status: IStatus | any = {}
  type: any;

  projectId : any = this.activatedRoute.snapshot.paramMap.get('id');
  members: number = 0
  tasks: number = 0
  error: any = {};


  ngOnInit(): void{
    this.projectService.getProjectById(this.projectId).subscribe((res: IResult) =>{
        if (res.result){
          this.project = res.data

          this.statusService.getStatusTypeById(this.project.statusId).subscribe((res: IResult) =>{
            this.status = res.data
          })
        }else{
          alert('Else blok executed')
        }
      }, (error) =>{
        this.error = {}

        if (error.status === 400 || error.status === 401) {
          for (let key in error.error.error) {
            console.log(error.error.error[key]);
            this.error[key] = error.error.error[key];
          }
        } else {
          alert("Unexpected error occurred: " + error.message);
        }
      }
    );

    this.projectService.getMemberCountByProjectId(this.projectId).subscribe((res: IResult) =>{
      if (res.result){
        this.members = res.data
      }else{
        alert('Else blok executed')
      }
    }, (error) =>{
      this.error = {}

      if (error.status === 400 || error.status === 401) {
        for (let key in error.error.error) {
          console.log(error.error.error[key]);
          this.error[key] = error.error.error[key];
        }
      } else {
        alert("Unexpected error occurred: " + error.message);
      }
    }
  );

  this.projectService.getTaskCountByProjectId(this.projectId).subscribe((res: IResult) =>{
    if (res.result){
      this.tasks = res.data
    }else{
      alert('Else blok executed')
    }
  }, (error) =>{
    this.error = {}

    if (error.status === 400 || error.status === 401) {
      for (let key in error.error.error) {
        console.log(error.error.error[key]);
        this.error[key] = error.error.error[key];
      }
    } else {
      alert("Unexpected error occurred: " + error.message);
    }
  }
);

}

viewTask(Id : number){
  this.router.navigate([`/Task`])
  this.projectService.projectId.next(Id);
}

}
