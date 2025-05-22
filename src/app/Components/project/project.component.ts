import { Component, inject } from '@angular/core';
import { ProjectService } from '../../Services/project.service';
import { SignUpService } from '../../Services/sign-up.service';
import { jwtDecode } from 'jwt-decode';
import { IProject } from '../../Models/Project.model';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { CdkTableModule } from '@angular/cdk/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateProjectComponent } from '../create-project/create-project.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';
import { IResult } from '../../Models/userLoginData.model';

@Component({
  selector: 'app-project',
  providers:[

            ],
  imports: [MatTableModule,
            MatButtonModule,
            CdkTableModule,
            MatDialogModule,
            MatMenuModule,
            MatIconModule
  ],
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {

  projectService = inject(ProjectService);
  signupService = inject(SignUpService);
  readonly dialog = inject(MatDialog);
  project: IProject | any = []
  router = inject(Router);
  
  token: any = this.signupService.getToken()
  decoded: any = jwtDecode(this.token);
  error: any = {};

  displayedColumns: string[] = ['No', 'projectName', 'dueDate','menu',]
  ngOnInit() : void{
    this.projectService.getProjectByUserId(this.decoded.UserId).subscribe(
      (res: IResult) => {
        if (res.result){
          this.project = res.data
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

  openDialog() {
    const dialogRef = this.dialog.open(CreateProjectComponent);

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  viewProject(id: number){
    this.router.navigate([`/ViewProject/${id}`])
  }
}
