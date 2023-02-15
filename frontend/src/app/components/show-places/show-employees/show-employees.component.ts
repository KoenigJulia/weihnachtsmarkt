import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { VendorService } from 'src/app/services/vendor.service';
import { Employee } from './../../../models/employee.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-show-employees',
  templateUrl: './show-employees.component.html',
  styleUrls: ['./show-employees.component.scss']
})
export class ShowEmployeesComponent implements OnInit {

  employees: Employee [] = []
  clickedEmployee: Employee | null = null

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.reload()
  }

  reload():void{
    this.data.vendorService.getAllEmployees().subscribe({
      next: (data: { employees: Employee[]; }) => {
        this.employees = data.employees
      },
      error: (error: any) => {

      }
    })
  }

  close():void{
    this.data.dialog.closeAll()
  }

  addEmployee():void{
    this.data.addDialog.open(AddEmployeeComponent, { data: { vendorId: this.data.clickedPlace.vendorId, vendorService: this.data.vendorService, dialog: this.data.addDialog } })
    .afterClosed().subscribe(() => this.reload())
  }
}
