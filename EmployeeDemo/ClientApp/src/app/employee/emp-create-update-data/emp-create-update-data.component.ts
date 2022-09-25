import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeDataService } from '../../service/employee-data.service';

@Component({
  selector: 'app-emp-create-update-data',
  templateUrl: './emp-create-update-data.component.html',
  styleUrls: ['./emp-create-update-data.component.css']
})
export class EmpCreateUpdateDataComponent implements OnInit {
  Departments: any[] = []
  myForm: FormGroup = {} as FormGroup;
  EmployeeIdRoute: any;
  emplyee:any

  constructor(private _employeeDataService: EmployeeDataService, private router: Router, private Activatedroute: ActivatedRoute) { }

  ngOnInit() {
    this.GetAllDepartment()
    this.Activatedroute.queryParamMap
      .subscribe(params => {

        this.EmployeeIdRoute = params.has('empId') ?  params.get('empId'):0;
        if (this.EmployeeIdRoute) {
          this.myForm = new FormGroup({
            id: new FormControl(''),
            fullname: new FormControl('', Validators.required),
            salary: new FormControl('', Validators.required),
            gender: new FormControl('', Validators.required),
            department: new FormControl('', Validators.required),
          });
          this.Getempdata(+ this.EmployeeIdRoute)
        

        } else {
          this.myForm = new FormGroup({
            id: new FormControl(''),
            fullname: new FormControl('', Validators.required),
            username: new FormControl('', Validators.required),
            salary: new FormControl('', Validators.required),
            gender: new FormControl('', Validators.required),
            department: new FormControl('', Validators.required),
            password: new FormControl('', Validators.required),
          });

        }

        
      });
 
  }
 

  GetAllDepartment() {
    this._employeeDataService.GetAllDepartment().subscribe((data: any) => {
      this.Departments = data;
    })
  }


  Getempdata(id: number) {
    this._employeeDataService.GetEmployee(id).subscribe((data: any) => {
      this.emplyee = data;

      this.myForm.patchValue({
        id: this.emplyee.id,
        fullname: this.emplyee.fullName,
        salary: this.emplyee.salary,
        gender: this.emplyee.gender,
        department: this.emplyee.departmentId
        
})
    
    })
  }

  onSubmit() {
   if( this.myForm.invalid){
      return;
    }

    if (this.EmployeeIdRoute) {
      const employee = {
        "Id": this.myForm.get("id")?.value,
        "fullName": this.myForm.get("fullname")?.value,
        "salary": this.myForm.get("salary")?.value,
        "gender": this.myForm.get("gender")?.value,
        "departmentId": this.myForm.get("department")?.value,
      }
      this._employeeDataService.UpdateEmployee(employee).subscribe((data: any) => {
        alert("user update sucess ")
        this.router.navigate(["employee/list"]);
      })
    } else {
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

}
