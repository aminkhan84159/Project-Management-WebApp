import { Component, inject, OnInit } from '@angular/core';
import { TaskService } from '../../Services/task.service';
import { ITask } from '../../Models/task.model';
import { jwtDecode } from 'jwt-decode';
import { SignUpService } from '../../Services/sign-up.service';
import { ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DATE_LOCALE, provideNativeDateAdapter } from '@angular/material/core';
import { DatePipe } from '@angular/common';
import { IResult } from '../../Models/userLoginData.model';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ProjectService } from '../../Services/project.service';

@Component({
  selector: 'app-create-task',
  providers: [provideNativeDateAdapter(),
              { provide: MAT_DATE_LOCALE, useValue: 'en-Gb'},
              DatePipe

              ],
  imports: [FormsModule,
            MatFormFieldModule,
            MatInputModule,
            MatDatepickerModule,
            MatSelectModule,
            MatDialogModule,
            MatButtonModule
  ],
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent implements OnInit {

  taskService = inject(TaskService)
  signUpService = inject(SignUpService)
  activatedRoute = inject(ActivatedRoute)
  projectService = inject(ProjectService)

  token: any = this.signUpService.getToken()
  decoded : any = jwtDecode(this.token)
  userId : number = this.decoded.UserId
  projectId : number = 0 ;
  taskObj: ITask | any = {}
  error: any = {};

  sDate: any
  eDate: any

  ngOnInit() : void{
    this.projectService.projectId$.subscribe(res =>{
      this.projectId = res;
    })
  }

  onCreate(){
    this.sDate = JSON.stringify(this.taskObj.startDate)?.slice(1,11)
    this.taskObj.startDate = this.sDate

    this.eDate = JSON.stringify(this.taskObj.endDate)?.slice(1,11)
    this.taskObj.endDate = this.eDate

    this.taskObj.projectId = this.projectId
    this.taskObj.userId = this.userId
    this.taskObj.statusId = 101

    this.taskService.createTask(this.taskObj).subscribe((res: IResult) =>{
      if(res.result){
        alert("Task Created Successfully ");
        this.error={};
      }else{
        alert("Else block executed");
      }
    }, (error) => {
      this.error = {};
      if (error.status === 400) {
        for (let key in error.error.error) {
          this.error[key] = error.error.error[key];       //Store each error with its key
        }
      }
      else if(error.status === 401){
        this.error={"Unauthorized ":" Unauthorized error"};
      }
      else {
        alert("Unexpected error occured"+error.message);
      }
    }
  )
  }
}

