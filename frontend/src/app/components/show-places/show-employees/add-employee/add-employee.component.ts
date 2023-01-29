import { Employee } from './../../../../models/employee.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.scss']
})
export class AddEmployeeComponent implements OnInit {

  addedEmployee: Employee = new Employee

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

  close():void{
    this.data.dialog.closeAll()
  }

  addEmployee():void{
    this.data.vendorService.addEmployee(this.addedEmployee, this.data.vendorId).subscribe({
      next: (data: any) =>{
        this.data.dialog.closeAll()
      },
      error: (error: any) =>{
        alert("Saving employee failed! " + error.message)
      }
    })
  }

}
