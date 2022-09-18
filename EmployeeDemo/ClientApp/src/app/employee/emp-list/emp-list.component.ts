import { Component, OnInit } from '@angular/core';
import {   Router } from '@angular/router';
import { EmployeeDataService } from '../../service/employee-data.service';

@Component({
  selector: 'app-emp-list',
  templateUrl: './emp-list.component.html',
  styleUrls: ['./emp-list.component.css']
})
export class EmpListComponent implements OnInit {
  employes: any[]=[]

  constructor(private _employeeDataService: EmployeeDataService, private router: Router) { }

  ngOnInit(): void {
    this.GetData();
  }

  GetData() {
    this._employeeDataService.GetAllEmployee().subscribe((data:any) => {
      this.employes = data;

    })
  }
  view(id: number) {
    this.router.navigate(["profile", id]);

  }
  delete(id: number) {
    var retVal = confirm("Do you want to Delete this Employee ?");
    if (retVal) {
      this._employeeDataService.deleteEmployee(id).subscribe((data: any) => {
        if (data) {
          this.GetData()
        }
       

      })
    }
  }

  addnewemp() {
    this.router.navigate(["create-update"]);
  }

}
