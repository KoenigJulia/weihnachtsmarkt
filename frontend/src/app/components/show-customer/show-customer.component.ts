import { ProductService } from './../../services/product.service';
import { AddProductOrderComponent } from './add-product-order/add-product-order.component';
import { ShowOrderProductsComponent } from './show-order-products/show-order-products.component';
import { AddOrderComponent } from './../add-order/add-order.component';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from './../../services/customer.service';
import { Order } from './../../models/order.model';
import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-show-customer',
  templateUrl: './show-customer.component.html',
  styleUrls: ['./show-customer.component.scss']
})
export class ShowCustomerComponent implements OnInit {

  orders: Order[] = []
  customerId: string = ""
  clickedOrder: Order | null = null
  orderPrices: number[] = []

  constructor(private route: ActivatedRoute, private customerService: CustomerService, 
    private orderService: OrderService, private router: Router, private dialog: MatDialog,
    private productService: ProductService){}

  ngOnInit(): void {
    this.route.params.subscribe(params => { this.customerId = params['id'] })
    this.reloadOrders()
  }

  reloadOrders():void{
    this.orders = []
    this.orderPrices = []
    this.orderService.getAll().subscribe({
      next: data => {
        this.orders = data.filter(o => o.customerId == this.customerId)
        this.reloadPrices()
      },
      error: error =>{

      }
    })
  }

  reloadPrices():void{
    this.orders.forEach(element => {
      this.getOrderPrice(element.id)
    });
  }

  getPrice(index: number):any{
    var price = this.orderPrices[index]
    return price
  }

  getOrderPrice(id: string):any{
    this.orderService.getOrderPrice(id).subscribe({
      next: data => {
        this.orderPrices.push(data)
      },
      error: error =>{

      }
    })
  }

  logout():void{
    this.router.navigate(['/loginCustomer'])
  }

  addOrder():void{
    this.dialog.open(AddOrderComponent, { data: { orderService: this.orderService, customerId: this.customerId, dialog: this.dialog } })
    .afterClosed().subscribe(() => this.reloadOrders())
  }

  showProducts(id: string):void{
    this.dialog.open(ShowOrderProductsComponent, { data: { orderService: this.orderService, orderId: this.clickedOrder!.id, dialog: this.dialog } })
    .afterClosed().subscribe(() => this.reloadOrders())
  }

  deleteOrder(id: string):void{
    this.orderService.deleteOrder(id).subscribe({
      next: data => {
        this.reloadOrders()
      },
      error: error =>{

      }
    })
  }

  addProduct(id: string):void{
    this.dialog.open(AddProductOrderComponent, { data: { orderService: this.orderService, productService: this.productService, orderId: this.clickedOrder!.id, dialog: this.dialog } })
    .afterClosed().subscribe(() => this.reloadOrders())
  }

}
