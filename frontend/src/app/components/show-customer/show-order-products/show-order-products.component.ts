import { Product } from 'src/app/models/product.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-show-order-products',
  templateUrl: './show-order-products.component.html',
  styleUrls: ['./show-order-products.component.scss']
})
export class ShowOrderProductsComponent implements OnInit {

  products: Product[] = []
  clickedProduct: Product | null = null

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.reload()
  }

  reload():void{
    this.data.orderService.getProductsByOrderId(this.data.orderId).subscribe({
      next: (data: Product[]) =>{
        this.products = data
      },
      error: (error: any) => {
        alert("Loading products failed! " + error.message)
      }
    })
  }

  close():void{
    this.data.dialog.closeAll()
  }

  deleteProduct(id: string):void{
    this.data.orderService.deleteProduct(this.data.orderId, this.clickedProduct?.id).subscribe({
      next: (data: Product[]) =>{
        this.reload()
      },
      error: (error: any) => {
        alert("Deleting product failed! " + error.message)
      }
    })
  }
}
