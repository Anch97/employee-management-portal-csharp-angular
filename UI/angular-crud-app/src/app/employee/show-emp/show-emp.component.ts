import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.scss']
})
export class ShowEmpComponent implements OnInit {

  employeeList: any = [];
  modalTitle: string = '';
  activateAddEditEmpComp: boolean = false;
  emp: any;

  // we need to use api method written in sharedservice file to fill the employee list array
  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.refreshEmpList();
  }

  // to refresh the employee list variable from api method
  refreshEmpList() {
    this.sharedService.getEmpList().subscribe(data => {
      this.employeeList = data;
    });
  }

  addClick() {
    this.emp = {
      EmployeeId:  0,
      EmployeeName: '',
      Department: '',
      DateOfJoining: '',
      PhotoFileName: 'anonymous.png'
    }
    this.modalTitle = 'Add Employee';
    this.activateAddEditEmpComp = true;
  }

  editClick(item: any) {
    this.emp = item;
    this.modalTitle = 'Edit Employee';
    this.activateAddEditEmpComp = true;
  }

  closeClick() {
    this.activateAddEditEmpComp = false;
    this.refreshEmpList();
  }

  deleteClick(item: any) {
    if(confirm('Are you sure?')) {
      this.sharedService.deleteEmployee(item.EmployeeId).subscribe(data => {
        alert(data.toString());
        this.refreshEmpList();
      })
    }
  }
}
