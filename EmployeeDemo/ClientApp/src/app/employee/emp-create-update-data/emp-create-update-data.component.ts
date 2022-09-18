import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeDataService } from '../../service/employee-data.service';

@Component({
  selector: 'app-emp-create-update-data',
  templateUrl: './emp-create-update-data.component.html',
  styleUrls: ['./emp-create-update-data.component.css']
})
export class EmpCreateUpdateDataComponent implements OnInit {
  Departments: any[] = []
  myForm: FormGroup = {} as FormGroup;

  constructor(private _employeeDataService: EmployeeDataService, private router: Router) { }

  ngOnInit() {
    this.GetAllDepartment()
    this.myForm = new FormGroup({
      fullname: new FormControl('', Validators.required),
      username: new FormControl('', Validators.required),
      salary: new FormControl('', Validators.required),
      gender: new FormControl('', Validators.required),
      department: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }
 

  GetAllDepartment() {
    this._employeeDataService.GetAllDepartment().subscribe((data: any) => {
      this.Departments = data;
    })
  }

  onSubmit() {
   if( this.myForm.invalid){
      return;
    }
    const employee = {
      "fullName": this.myForm.get("fullname")?.value,
      "username": this.myForm.get("username")?.value,
      "Password": this.myForm.get("password")?.value,
      "salary": this.myForm.get("salary")?.value,
      "gender": this.myForm.get("gender")?.value,
      "departmentId": this.myForm.get("department")?.value,
    }

    this._employeeDataService.AddEmployee(employee).subscribe((data: any) => {
      alert("user created sucess please login")
      this.router.navigate(["login"]);
    })
  }

}
