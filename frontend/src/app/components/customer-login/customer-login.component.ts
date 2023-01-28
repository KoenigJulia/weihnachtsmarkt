import { Router } from '@angular/router';
import { CustomerService } from './../../services/customer.service';
import { AddCustomer } from './../../models/customer.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.scss']
})
export class CustomerLoginComponent implements OnInit {

  customerId: string = ""
  customer: AddCustomer = new AddCustomer;

  constructor(private customerService: CustomerService, private router: Router) { }

  ngOnInit(): void {
  }

  login():void{
    this.customerService.getCustomerById(this.customerId).subscribe({
      next: (data: any) =>{
        this.router.navigate(['showCustomer/' + data.id])
      },
      error: (error: any) =>{
        alert("No customer with this id! " + error.message)
      }
    })
  }

  create():void{
    this.customerService.addCustomer(this.customer).subscribe({
      next: (data: any) =>{
        this.router.navigate(['showCustomer/' + data.id])
      },
      error: (error: any) =>{
        alert("Add customer failed! " +error.message)
      }
    })
  }
}
