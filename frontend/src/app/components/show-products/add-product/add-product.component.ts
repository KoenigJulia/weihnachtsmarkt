import { Vendor } from 'src/app/models/vendor.model';
import { AddProduct } from './../../../models/product.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.scss']
})
export class AddProductComponent implements OnInit {

  vendor: Vendor | null = null;
  product: AddProduct = new AddProduct;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.vendor = this.data.vendor
  }

  addProduct():void{
    this.product.vendorId = this.vendor!.id
    this.data.productService.addProduct(this.product).subscribe({
      next: (data: any) =>{
        this.data.dialog.closeAll()
      },
      error: (error: { message: string; }) => {
        alert("Saving product failed! " + error.message);
      }
    })
  }

  close():void{
    this.data.dialog.closeAll()
  }
}
