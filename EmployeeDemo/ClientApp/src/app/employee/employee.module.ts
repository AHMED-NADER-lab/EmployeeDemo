import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { EmpListComponent } from './emp-list/emp-list.component';
import { EmpCreateUpdateDataComponent } from './emp-create-update-data/emp-create-update-data.component';
import { EmpProfileComponent } from './emp-profile/emp-profile.component';
import { ReactiveFormsModule } from '@angular/forms';




@NgModule({
  declarations: [
    EmpListComponent,
    EmpCreateUpdateDataComponent,
    EmpProfileComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: '', component: EmpListComponent, pathMatch: 'full' },
      { path: 'list', component: EmpListComponent },
      { path: 'create-update', component: EmpCreateUpdateDataComponent },
      { path: 'profile/:id', component: EmpProfileComponent },

    ])
  ]
})
export class EmployeeModule { }
