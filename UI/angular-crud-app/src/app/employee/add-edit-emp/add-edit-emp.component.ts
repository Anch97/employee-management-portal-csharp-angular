import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.scss']
})
export class AddEditEmpComponent implements OnInit {

  @Input() emp: any;

  EmployeeId: string = '';
  EmployeeName: string = '';
  Department: string = '';
  DateOfJoining: string = '';
  PhotoFileName: string = '';
  PhotoFilePath: string = '';
  DepartmentList: any = [];

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.loadDepartmentList();
  }

  loadDepartmentList() {
    this.sharedService.getAllDepartmentNames().subscribe((data: any) => {
      this.DepartmentList = data;

      this.EmployeeId = this.emp.EmployeeId;
      this.EmployeeName = this.emp.EmployeeName;
      this.Department = this.emp.Department;
      this.DateOfJoining = this.emp.DateOfJoining;
      this.PhotoFileName = this.emp.PhotoFileName;
      this.PhotoFilePath = this.sharedService.PhotoUrl+this.PhotoFileName;
    });
  }

  addEmployee() {
    let val  = {
      EmployeeId: this.EmployeeId,
      EmployeeName: this.EmployeeName,
      Department: this.Department,
      DateOfJoining: this.DateOfJoining,
      PhotoFileName: this.PhotoFileName
    };
    this.sharedService.addEmployee(val).subscribe(data => {
      alert(data.toString());
    });
  }

  updateEmployee() {
    let val  = {
      EmployeeId: this.EmployeeId,
      EmployeeName: this.EmployeeName,
      Department: this.Department,
      DateOfJoining: this.DateOfJoining,
      PhotoFileName: this.PhotoFileName
    };
    this.sharedService.updateEmployee(val).subscribe(data => {
      alert(data.toString());
    });
  }
  // saving uploaded profile photo
  uploadPhoto(event: any) {
    let file = event.target.files[0];
    const formData: FormData = new FormData();
    formData.append('uploadedFile', file, file.name);

    // sending this form data to api method
    this.sharedService.uploadPhoto(formData).subscribe((data: any) => {
      this.PhotoFileName = data.toString();
      this.PhotoFilePath = this.sharedService.PhotoUrl+this.PhotoFileName;
    })
  }


}
