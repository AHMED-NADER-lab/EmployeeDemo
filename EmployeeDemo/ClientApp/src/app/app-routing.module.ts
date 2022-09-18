import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';




const routes: Routes = [

  {
    path: '',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule) ,

  },
  {
    path: 'login',
    loadChildren: () => import('./login/login.module').then(m => m.LoginModule)
  },



  {
    path: 'employee',
    loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule)
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
