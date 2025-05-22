import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatFormField, MatInputModule } from '@angular/material/input';
import { MatDatepickerModule, MatDatepickerToggle } from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';
import { ProjectService } from '../../Services/project.service';
import { IProject } from '../../Models/Project.model';
import { MAT_DATE_LOCALE } from '@angular/material/core'
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { IResult } from '../../Models/userLoginData.model';

@Component({
  selector: 'app-create-project',
  providers: [provideNativeDateAdapter(),
             { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
             DatePipe,
             ],
  imports: [FormsModule,
            MatInputModule,
            MatFormField,
            MatDatepickerModule,
            MatDialogModule,
            MatButtonModule
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './create-project.component.html',
  styleUrl: './create-project.component.css'
})
export class CreateProjectComponent {

  projectService = inject(ProjectService);
  projectObj : IProject | any = {}
  error: any = {};
  date: any;

  onCreate(){
    this.date = JSON.stringify(this.projectObj.dueDate)?.slice(1,11)
    this.projectObj.dueDate = this.date
    console.log(this.projectObj.dueDate)

    this.projectObj.statusId = 101
    console.log(this.projectObj)
    this.projectService.createProject(this.projectObj).subscribe((res:IResult)=> {
      if(res.result==true){
        alert("project Created Successfully ");
        this.error={};
      }
      else{
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
    })
  }

}
