import { Component, inject, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormField, MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { userDetails } from '../../Models/userDetails.model';
import { userSignupData } from '../../Models/userSignUpData.model';
import { SignUpService } from '../../Services/sign-up.service';
import { UserDetailsService } from '../../Services/user-details.service';
import { FormsModule, NgForm } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { Router } from '@angular/router';
import { IResult } from '../../Models/userLoginData.model';

@Component({
  selector: 'app-sign-up',
  imports: [MatFormField,
            MatIconModule,
            MatLabel,
            MatIconModule,
            MatInputModule,
            MatButtonModule,
            FormsModule,
            MatRadioModule,
            MatFormFieldModule,
            MatInputModule
  ],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {

  userDetails = false;
  IsMatch : boolean = false;
  userDetailsObj: userDetails = new userDetails();
  userSignupDataObj : userSignupData = new userSignupData();
  signupService = inject(SignUpService);
  userDetailService = inject(UserDetailsService);
  router= inject(Router);
  error: { [key: string]: string } = {};

  hide = signal(true);
  clickEvent(event: MouseEvent) {
    this.hide.set(!this.hide());
    event.stopPropagation();
  }

  comparePassword(pws: string, cPws: string) {
    if (pws !== cPws) {
      this.IsMatch = true;
    }
    else {
      this.IsMatch = false;
    }
  }


  signup(){
    this.userDetails = true;
    this.signupService.addSignupUser(this.userSignupDataObj).subscribe((res: IResult) =>{
      if (res.result == true) {
        alert("Signup done Successfully !!");
        this.userSignupDataObj = new userSignupData();
        this.signupService.setToken(res.data);      
        console.log(res.data);
        this.error = {};                  //THIS WILL KEEP ERROR OBJECT EMPTY SO PREVIOUS ERROR WILL NOT BE DISPLAYED
      }
      else {
        alert("Else block executed");
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

  onRegister() {
    this.signupService.addRegisterUser(this.userDetailsObj).subscribe((res: IResult) => {
      if (res.result == true) {
        this.signupService.isLoggedIn.next(true);
        alert("register successful");
        this.userDetailsObj = new userDetails();
        this.error = {};          //THIS WILL KEEP ERROR OBJECT EMPTY SO PREVIOUS ERROR WILL NOT BE DISPLAYED
        this.router.navigate(['/Home']).then(() => {
          window.location.reload()});
      }
      else {
        alert("Else block executed");
      }
    }, (error) => {
      this.error = {};
      if (error.status === 400 || error.status === 401) {
        for (let key in error.error.error) {
          console.log(error.error.error[key]);
          this.error[key] = error.error.error[key];       //Store each error with its key
        }

      }
      else {
        alert("Unexpected error occured"+error.message);
      }
      console.log("User Detail Data: ", this.userDetailsObj);
    })
  }
}
