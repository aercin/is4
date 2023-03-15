import { Injectable } from '@angular/core'
import {
    HttpInterceptor,
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators'; 
import {AuthService} from '../auth/auth.service';  
import { EMPTY } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
   
    constructor(private authService: AuthService,private router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {  
        let authReq = request;  
        var access_token = localStorage.getItem('access-token');
        if (!authReq.url.includes('connect/token') && access_token) {
            authReq = this.addTokenHeader(request, access_token);
        }
 
        return next.handle(authReq).pipe(catchError((err: HttpErrorResponse) => { 
            if (err.status === 401) {
              return this.handle401Error(authReq, next);
            }
            if(err.status === 400 && err.error.error.toString() ==='invalid_grant')
            {
                localStorage.removeItem('access-token');
                localStorage.removeItem('refresh-token');
                this.router.navigate(['']);  
                return EMPTY;
            }  
             return throwError(err);
          }));
    }     

    private addTokenHeader(request: HttpRequest<any>, access_token: string) { 
       return request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + access_token) });
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler)  {
        
        return this.authService.silentSignIn().pipe( 
            switchMap(response => {  
                localStorage.setItem('access-token',response.access_token);
                localStorage.setItem('refresh-token',response.refresh_token);
             
                return next.handle(this.addTokenHeader(request, response.access_token));
            }) 
       );
   }   
}
