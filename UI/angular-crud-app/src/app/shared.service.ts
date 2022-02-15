import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = 'http://localhost:5000/api';
  readonly PhotoUrl = 'http://localhost:5000/Photos';

  constructor(private http: HttpClient) { }

  //method to consume get department api to get info about departments
  getDepList() : Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/department');
  }

  // method to consume post department api to add new department
  addDepartment(val: any) {
    return this.http.post(this.APIUrl + '/department', val);
  }

  // method to consume put department api to update existing department
  updateDepartment(val: any) {
    return this.http.put(this.APIUrl + '/department', val);
  }

  // method to consume put department api to update existing department
  deleteDepartment(val: any) {
    return this.http.delete(this.APIUrl + '/department/'+val);
  }

  //method to consume get employee api to get info about employee
  getEmpList() : Observable<any[]> {
    return this.http.get<any>(this.APIUrl + '/employee');
  }

  // method to consume post employee api to add new employee
  addEmployee(val: any) {
    return this.http.post(this.APIUrl + '/employee', val);
  }

  // method to consume put employee api to update existing employee
  updateEmployee(val: any) {
    return this.http.put(this.APIUrl + '/employee', val);
  }

  // method to consume put employee api to update existing employee
  deleteEmployee(val: any) {
    return this.http.delete(this.APIUrl + '/employee/'+val);
  }

  // method to save profile pictures
  uploadPhoto(val: any){
    return this.http.post(this.APIUrl + '/employee/savefile', val);
  }

  // method to get all department names
  getAllDepartmentNames() : Observable<any[]> {
    return this.http.get<any[]>(this.APIUrl + '/employee/getalldepartmentnames');
  }
}
