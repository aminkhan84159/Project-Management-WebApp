import { Component, inject } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NavBarComponent } from "./Components/nav-bar/nav-bar.component";
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { UserDetailsService } from './Services/user-details.service';
import { SignUpService } from './Services/sign-up.service';
import { IUser } from './Models/userDetails.model';
import { jwtDecode } from 'jwt-decode';
import { IResult } from './Models/userLoginData.model';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, 
            NavBarComponent,
            MatSidenavModule,
            MatListModule,
            MatIconModule,
            MatButtonModule,
            RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Nexus.UI';

  signupService = inject(SignUpService)
  useService = inject(UserDetailsService)
  user : IUser | any = {}

  token : string | null = null;
  decoded : any = null;
  error: any = {}


  ngOnInit(): void{
    this.token = this.signupService.getToken();
    console.log(this.token);

    if (this.token !== null) {
        this.decoded = jwtDecode(this.token); 
        this.useService.getUserDetailById(this.decoded.UserId).subscribe((res: IResult) => {
          this.user = res.data;
        },(error) => {
          console.log(error);
        });
    } else {
      console.log('No token found in localStorage.');
    }
  }

}
