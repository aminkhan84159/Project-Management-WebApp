import { ChangeDetectionStrategy, Component, inject, signal, ViewEncapsulation } from '@angular/core';
import { MatFormFieldModule} from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule} from '@angular/material/button';
import { MatInputModule} from '@angular/material/input';
import { FormControl, NgForm, FormGroup, FormsModule } from '@angular/forms';
import { IResult, UserLoginData } from '../../Models/userLoginData.model';
import { Router } from '@angular/router';
import { LoginService } from '../../Services/login.service';
import { SignUpService } from '../../Services/sign-up.service';
import { error } from 'console';

@Component({
  selector: 'app-login',
  imports: [MatFormFieldModule,
            MatIconModule,
            MatFormFieldModule,
            MatButtonModule,
            MatInputModule,
            FormsModule
            ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  encapsulation : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {

  router = inject(Router);
  userLoginService = inject(LoginService);
  userSignupService = inject(SignUpService);
  error: { [key: string]: string } = {};

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  loginForm : UserLoginData = new UserLoginData();

  onLogin(){
    this.userLoginService.userLogin(this.loginForm).subscribe((res: IResult) =>{
      if (res.result == true){
        this.userSignupService.isLoggedIn.next(true);
        this.loginForm = new UserLoginData();
        this.userSignupService.setToken(res.data);
        this.router.navigate(['/Home'])
      }
      else{
        console.log(res.data);
        console.log(res.message);
      }
    }, (error) => {
      if(error.status === 400){
        for(let key in error.error.error){
          console.log(error.error.error[key]);
          this.error[key] = error.error.error[key];     
        }
      }
      else{
        alert("unexpected error occurec in error block "+ error.message);
      }
    })
  }

}
