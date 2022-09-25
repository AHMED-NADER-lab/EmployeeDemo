import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard, LoginGuard } from './Gurad/Auth-Guard';




const routes: Routes = [

  {
    path: '',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule),
    canActivate: [LoginGuard]

  },
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule),
    canActivate: [LoginGuard]
  },



  {
    path: 'employee',
    loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule),
    canActivate: [AuthGuard]
  },
  //{
  //  canActivateChild: [AuthorizeServiceGuard],
  //  path: 'initiative',
  //  loadChildren: () => import('./initiative-application/initiative-application.module').then(m => m.InitiativeApplicationModule), data: {
  //    breadcrumb: { skip: true }
  //  }
  //  ,

  //},
 
];
//canActivate: [AuthorizeGuard]
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
