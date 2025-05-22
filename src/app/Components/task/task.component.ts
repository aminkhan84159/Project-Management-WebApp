import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IStatus } from '../../Models/status.model';
import { TaskService } from '../../Services/task.service';
import { IResult } from '../../Models/userLoginData.model';
import { MatTableModule } from '@angular/material/table';
import { ITask } from '../../Models/task.model';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { MatButtonModule } from '@angular/material/button';
import { ProjectService } from '../../Services/project.service';

@Component({
  selector: 'app-task',
  imports: [MatTableModule,
            MatDividerModule,
            MatDialogModule,
            MatButtonModule

  ],
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent {

  activatedRoute = inject(ActivatedRoute)
  taskService = inject(TaskService)
  projectService = inject(ProjectService)
  readonly dialog = inject(MatDialog);


  task : ITask | any = []
  projectId : any ;
  error: any = {};

  displayedColumns: string[] = ['No','Name','Description','Priority','Type','StartDate','EndDate']

  ngOnInit() : void{

    this.projectService.projectId$.subscribe((res )=>{
      this.projectId = res
    })

    this.taskService.getTaskByProjectId(this.projectId).subscribe((res: IResult) =>{
      if(res.result){
        this.task = res.data
      }else{
        console.log(res.error)
      }
    },(error) => {
      this.error = {};
      if (error.status === 400) {
        for (let key in error.error.error) {
          console.log(error.error.error[key]);
          this.error[key] = error.error.error[key];         //Store each error with its key
        }
      }
      else {
        alert("Unexpected error occured " + error.message);
      }
    })
  }

  openDialog() {
      const dialogRef = this.dialog.open(CreateTaskComponent);
  
      dialogRef.afterClosed().subscribe(result => {
      });
  }
}
