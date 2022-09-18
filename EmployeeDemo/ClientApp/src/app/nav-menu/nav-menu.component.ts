import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeDataService } from '../service/employee-data.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  islogin = false;

  constructor(private router: Router, private _employeeDataService: EmployeeDataService) { }

  ngOnInit(): void  {
    const token = localStorage.getItem("token");
    if (token) {
      this.islogin=true
    }
    this._employeeDataService.subject$.subscribe((x:any) => {
      this.islogin=x
    })
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    localStorage.clear()
    this.router.navigate(["login"]);
  this.islogin = false;
  }
}
