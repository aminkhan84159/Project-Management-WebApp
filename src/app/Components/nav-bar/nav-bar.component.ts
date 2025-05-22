import { Component, inject, Output, EventEmitter, Inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { SignUpService } from '../../Services/sign-up.service';
import { IUser } from '../../Models/userDetails.model';
import { jwtDecode } from 'jwt-decode';
import { IResult } from '../../Models/userLoginData.model';
import { UserDetailsService } from '../../Services/user-details.service';



@Component({
  selector: 'app-nav-bar',
  imports: [MatIconModule,
            RouterLink,
            MatSidenavModule,
            MatToolbarModule,
            MatButtonModule,
            MatListModule,
            MatIconModule,
  ],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css',
})
export class NavBarComponent {

  @Output() toggleSidenavEvent = new EventEmitter<void>();

  router = inject(Router)
  signupService = inject(SignUpService)

  loggedIn : any;

  ngOnInit(): void{
    this.signupService.isLoggedIn$.subscribe((isLoggedIn) => {
      this.loggedIn = isLoggedIn;
      console.log(this.loggedIn);
    })
  }

  Logout(){
    this.signupService.isLoggedIn.next(false);
    this.signupService.removeToken();
    this.router.navigate(['/Home'])
  }

  toggleSidenav() {
    // this.router.navigate(['/Protected'])
    this.toggleSidenavEvent.emit();
  }
}
