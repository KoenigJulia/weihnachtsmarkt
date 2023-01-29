import { AddOrder } from './../../models/order.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.scss']
})
export class AddOrderComponent implements OnInit {

  order: AddOrder = new AddOrder;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

  addOrder():void{
    this.order.customerId = this.data.customerId
    this.order.products = []
    this.data.orderService.addOrder(this.order).subscribe({
      next: (data: any) =>{
        this.data.dialog.closeAll()
      },
      error: (error: { message: string; }) => {
        alert("Saving order failed! " + error.message);
      }
    })
  }

  close():void{
    this.data.dialog.closeAll()
  }

}
