import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 
import { Router } from '@angular/router';
import { AuthResponse } from './auth-response';
import { CheckPermissionResponse } from './check-permission-response'; 
import { environment } from '../../environments/environment'; 


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,private router: Router) {}


  signIn(username: string, password: string)  {
    let body = new URLSearchParams(); 
    body.set('client_id',  environment.client_id);
    body.set('client_secret', environment.client_secret);
    body.set('grant_type', 'password');
    body.set('username', username);
    body.set('password', password);

    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
  
   return this.http.post<AuthResponse>(environment.idp_base_url +'/connect/token', body.toString(), options);
  } 

  silentSignIn(){  
    let refreshToken = localStorage.getItem("refresh-token");
    if(Boolean(refreshToken) == false) 
      throw new Error('Refresh token is not found'); 

    let body = new URLSearchParams();
    body.set('client_id', environment.client_id);
    body.set('client_secret', environment.client_secret);
    body.set('grant_type', 'refresh_token');
    body.set('refresh_token', refreshToken || '');  

    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
 
    return this.http.post<AuthResponse>(environment.idp_base_url + '/connect/token', body.toString(), options);
  } 

  signOut()
  { 
    localStorage.removeItem('access-token');
    localStorage.removeItem('refresh-token');
    this.router.navigate(["/"]);
  }

  checkPermission(permission : string)
  {
    return this.http.get<CheckPermissionResponse>(environment.bff_base_url + '/api/permission-check?permission='+ permission)
  } 
}
