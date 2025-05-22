import { Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { NavBarComponent } from './Components/nav-bar/nav-bar.component';
import { AppComponent } from './app.component';
import { authGuard } from './Guard/auth-guard.guard';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },
    {
        path: 'Home',
        loadComponent: () => import('./Components/home/home.component').then(x => x.HomeComponent)
    },
    {
        path: 'AboutUs',
        loadComponent: () => import('./Components/about-us/about-us.component').then(x => x.AboutUsComponent)
    },
    {
        path: 'Login',
        loadComponent: () => import('./Components/login/login.component').then(x => x.LoginComponent)
    },
    {
        path: 'SignUp',
        loadComponent: () => import('./Components/sign-up/sign-up.component').then(x => x.SignUpComponent)
    },
    {
        path: 'Project',
        loadComponent: () => import('./Components/project/project.component').then(x => x.ProjectComponent)
    },
    {
        path: 'ViewProject/:id',
        loadComponent: () => import('./Components/view-project/view-project.component').then(x => x.ViewProjectComponent)
    },
    {
        path: 'Task',
        loadComponent: () => import('./Components/task/task.component').then(x => x.TaskComponent)
    }
];
