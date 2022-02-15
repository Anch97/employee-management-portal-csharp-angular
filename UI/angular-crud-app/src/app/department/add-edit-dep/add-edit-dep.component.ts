import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit-dep',
  templateUrl: './add-edit-dep.component.html',
  styleUrls: ['./add-edit-dep.component.scss']
})
export class AddEditDepComponent implements OnInit {

  @Input() dep: any;

  DepartmentId: string = '';
  DepartmentName: string = '';

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.DepartmentId = this.dep.DepartmentId;
    this.DepartmentName = this.dep.DepartmentName;
  }

  addDepartment() {
    let val  = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.sharedService.addDepartment(val).subscribe(data => {
      alert(data.toString());
    });
  }

  updateDepartment() {
    let val  = {
      DepartmentId: this.DepartmentId,
      DepartmentName: this.DepartmentName
    };
    this.sharedService.updateDepartment(val).subscribe(data => {
      alert(data.toString());
    });
  }

}
