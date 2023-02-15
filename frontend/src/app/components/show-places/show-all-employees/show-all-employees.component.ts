import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Employee } from 'src/app/models/employee.model';

@Component({
  selector: 'app-show-all-employees',
  templateUrl: './show-all-employees.component.html',
  styleUrls: ['./show-all-employees.component.scss']
})
export class ShowAllEmployeesComponent implements OnInit {

  employees: Employee [] = []
  clickedEmployee: Employee | null = null

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.reload()
  }

  reload():void{
    this.data.vendorService.getAllEmployees().subscribe({
      next: (data: Employee[]) => {
        this.employees = data
      },
      error: (error: any) => {

      }
    })
  }

  close():void{
    this.data.dialog.closeAll()
  }
}
