import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import {AuthService} from '../auth/auth.service';  
import { Observable,of } from 'rxjs';
import { catchError, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthzGuard implements CanActivate {

  constructor(private authservice: AuthService,private router:Router) {}

  canActivate(route: ActivatedRouteSnapshot) : Observable<boolean>{  
    if(route.data['expectedPermission'] === undefined)
    {
      return of(true);
    } 
    
    const expectedPermission = route.data['expectedPermission'];
  
    return this.authservice.checkPermission(expectedPermission)
                           .pipe(map((res) => {  
                             if(res.isSuccess){
                              return true;
                             }
                             else 
                             {
                                this.router.navigate(["/"]);
                                return false;
                             }
                           }) 
                           ,catchError(() => {  
                             this.router.navigate(["/"]);
                              return of(false);
                           })); 
  } 
}
