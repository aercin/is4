import { Component } from '@angular/core';
import { FormBuilder, Validators,FormGroup  } from '@angular/forms';
import { Router } from '@angular/router';
import {AuthService} from '../auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
 
  disableLogin: boolean = false;
  
  loginForm = this.fb.group({
    userName: ['',Validators.required],
    password: ['',Validators.required]
  });

  constructor(private fb: FormBuilder,private authService : AuthService,private router: Router) { }

  onSubmit(form: FormGroup): void { 
    this.disableLogin = true;

    this.authService.signIn(form.value.userName,form.value.password).subscribe(response => 
      { 
        localStorage.setItem('access-token',response.access_token);
        localStorage.setItem('refresh-token',response.refresh_token);
        
        this.router.navigate(['dashboard']);       
      },
      error => {
        this.disableLogin = false;
       // alert('hata aldık');
      });
  }
}
