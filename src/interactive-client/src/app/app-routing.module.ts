import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from './login/login.component';
import { DashboardComponent} from './dashboard/dashboard.component';
import {AuthzGuard} from './guards/authz.guard';

const routes: Routes = [
  { path: '', component: LoginComponent},
  {
    path:'dashboard',
    component:DashboardComponent,
    canActivate:[AuthzGuard],
    data: { 
       expectedPermission: 'admin'
    } 
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
