import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeDataService } from '../../service/employee-data.service';

@Component({
  selector: 'app-sigin',
  templateUrl: './sigin.component.html',
  styleUrls: ['./sigin.component.css']
})
export class SiginComponent implements OnInit {

  myForm: FormGroup = {} as FormGroup;

  constructor(private _employeeDataService: EmployeeDataService, private router: Router) { }

  ngOnInit() {
    this.myForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onSubmit() {
    if (this.myForm.invalid) {
      return;
    }
    const loginobj = {
      "username": this.myForm.get("username")?.value,
      "password": this.myForm.get("password")?.value

    }
    this._employeeDataService.login(loginobj).subscribe((data:any) => {
      if (data.islogin==true) {
        localStorage.setItem("token", data['token']);
        this.router.navigate(["employee/list"]);
        this._employeeDataService.subject$.next(true);
      }
     
    })
  }

}
