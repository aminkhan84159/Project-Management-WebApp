import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SignUpService } from '../Services/sign-up.service';

export const authGuard: CanActivateFn = (route, state) => {

  const signupService = inject(SignUpService)
  const router = inject(Router)

  if(signupService.getToken() !== null){
    return true
  }
  else{
    alert('Login first')
    router.navigate(['/Login'])
  return false;
  }
};
