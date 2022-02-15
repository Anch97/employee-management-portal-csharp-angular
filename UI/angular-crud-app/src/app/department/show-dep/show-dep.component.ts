import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.scss']
})
export class ShowDepComponent implements OnInit {

  departmentList: any = [];
  modalTitle: string = '';
  activateAddEditDepComp: boolean = false;
  dep: any;
  departmentIdFilter: string = '';
  departmentNameFilter: string = '';
  departmentListWithoutFilter: any = [];

  // we need to use api method written in sharedservice file to fill the department list array
  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.refreshDepList();
  }

  // to refresh the department list variable from api method
  refreshDepList() {
    this.sharedService.getDepList().subscribe(data => {
      this.departmentList = data;
      // backup list
      this.departmentListWithoutFilter = data;
    });
  }

  addClick() {
    this.dep = {
      DepartmentId:  0,
      DepartmentName: ''
    }
    this.modalTitle = 'Add Department';
    this.activateAddEditDepComp = true;
  }

  editClick(item: any) {
    this.dep = item;
    this.modalTitle = 'Edit Department';
    this.activateAddEditDepComp = true;
  }

  closeClick() {
    this.activateAddEditDepComp = false;
    this.refreshDepList();
  }

  deleteClick(item: any) {
    if(confirm('Are you sure?')) {
      this.sharedService.deleteDepartment(item.DepartmentId).subscribe(data => {
        alert(data.toString());
        this.refreshDepList();
      })
    }
  }

  filterFn() {
    let departmentIdFilter = this.departmentIdFilter;
    let departmentNameFilter = this.departmentNameFilter;

    this.departmentList = this.departmentListWithoutFilter.filter(function(el: any) {
      return el.departmentId.toString().toLowerCase().includes(departmentIdFilter.toString().trim().toLowerCase())
      && el.departmentName.toString().toLowerCase().includes(departmentNameFilter.toString().trim().toLowerCase())
    });
  }
}
