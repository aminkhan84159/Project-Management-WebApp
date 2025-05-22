import { Component, inject } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-about-us',
  imports: [MatIconModule],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {

  router = inject(Router)

  backClick(){
    this.router.navigate([''])
  }
}
