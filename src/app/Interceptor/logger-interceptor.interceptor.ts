import { HttpInterceptorFn } from '@angular/common/http';
import { SignUpService } from '../Services/sign-up.service';
import { inject } from '@angular/core';

export const loggerInterceptor: HttpInterceptorFn = (req, next) => {
  const userSignupService=inject(SignUpService);
  const token = userSignupService.getToken();
  // console.log(token);
  const authReq = req.clone({
    // headers: req.headers.set('Authorizatin', token),
    headers: req.headers.set('Authorization',` Bearer ${token}`).set('Content-Type', 'application/json') // Add this line
 
  });
  return next(authReq);
};
