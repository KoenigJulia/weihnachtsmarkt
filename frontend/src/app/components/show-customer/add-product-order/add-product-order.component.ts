import { AddProductToOrder } from './../../../models/product.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Product } from 'src/app/models/product.model';

@Component({
  selector: 'app-add-product-order',
  templateUrl: './add-product-order.component.html',
  styleUrls: ['./add-product-order.component.scss']
})
export class AddProductOrderComponent implements OnInit {
  selectedProduct:  string = "";
  addedProduct: AddProductToOrder = new AddProductToOrder
  products: Product[] = []

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.data.productService.getAll().subscribe({
      next: (data: Product[]) => {
        this.products = data
      },
      error : (error: any) => {
        alert("Loading products failed! " + error.message)
      }
    })
  }

  addProduct():void{
    if(this.selectedProduct != ""){
      this.addedProduct.productId = this.selectedProduct
      this.data.orderService.addProductToOrder(this.data.orderId,this.addedProduct).subscribe({
        next: (data: any) =>{
          this.data.dialog.closeAll()
        },
        error: (error: { message: string; }) => {
          alert("Saving product failed! " + error.message);
        }
      })
    }
  }

  close():void{
    this.data.dialog.closeAll()
  }

}
