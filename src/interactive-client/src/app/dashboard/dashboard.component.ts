import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {environment} from '../../environments/environment';
import {AuthService} from '../auth/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {

  constructor(private http: HttpClient, private authService : AuthService) {}

  goToMembershipBFF(){ 
     this.http.post(environment.idp_base_url + '/api/add/permission', {
      scope: 'sample-permission',
      description: 'sample-permission-desc'
    }).subscribe(response => 
    { 
        alert('işlem başarılı'+ JSON.stringify(response));
    },
    error => { 
      alert('hata aldık' + JSON.stringify(error));
    });
  } 

  goToLegacyResourceBFF(){
    this.http.get(environment.bff_base_url + '/api/protected-values').subscribe(response => 
    {  
        alert('işlem başarılı'+ JSON.stringify(response));
    },
    error => {  
      alert('hata aldık' + JSON.stringify(error));
    });
  }

  goToResourceBFF()
  {
    this.http.get(environment.bff_base_url + '/instant').subscribe(response => 
    {  
        alert('işlem başarılı'+ JSON.stringify(response));
    },
    error => {  
      alert('hata aldık' + JSON.stringify(error));
    });
  }

  logout(){
    this.authService.signOut();
  }
} 