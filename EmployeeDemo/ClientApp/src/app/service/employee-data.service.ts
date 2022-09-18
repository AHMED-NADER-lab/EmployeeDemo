import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EmployeeDataService {

  subject$ = new Subject();
  
  constructor(private http: HttpClient) { }

  GetAllEmployee() {
    return this.http.get('http://localhost:5276/Employee/GetAllEmployee')

  }

  GetEmployee(id:number) {
    return this.http.get('http://localhost:5276/Employee/' + id )

  }

  deleteEmployee(id: number) {
    return this.http.delete('http://localhost:5276/Employee/' + id )

  }


  GetAllDepartment() {
    return this.http.get('http://localhost:5276/Employee/GetAllDepartment' )

  }


  AddEmployee(emp:any) {
    return this.http.post('http://localhost:5276/Employee/Resgister', emp )

  }

  login(data: any) {
    return this.http.post('http://localhost:5276/Employee/Login', data)

  }
}
