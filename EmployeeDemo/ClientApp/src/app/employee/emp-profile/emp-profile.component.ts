import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EmployeeDataService } from '../../service/employee-data.service';

@Component({
  selector: 'app-emp-profile',
  templateUrl: './emp-profile.component.html',
  styleUrls: ['./emp-profile.component.css']
})
export class EmpProfileComponent implements OnInit {
  employee : any
  constructor(private _employeeDataService: EmployeeDataService, private actRoute: ActivatedRoute) { }
  ngOnInit(): void {
    this.actRoute.paramMap.subscribe((params) => {
      const empid = params.get('id')
      if (empid) {
        this.GetData(+empid);

      }
    });
  }


  GetData(id: number) {
    this._employeeDataService.GetEmployee(id).subscribe((data: any) => {
      this.employee = data;

    })
  }

}
