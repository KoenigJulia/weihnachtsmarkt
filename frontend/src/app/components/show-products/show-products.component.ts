import { VendorService } from 'src/app/services/vendor.service';
import { ProductService } from './../../services/product.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { AddProductComponent } from './add-product/add-product.component';
import { Vendor } from 'src/app/models/vendor.model';

@Component({
  selector: 'app-show-products',
  templateUrl: './show-products.component.html',
  styleUrls: ['./show-products.component.scss']
})
export class ShowProductsComponent implements OnInit {

  products: Product[] = []
  vendorId: string = "";
  vendor: Vendor | null = null;
  clickedProduct: Product | null = null;

  constructor(private router: ActivatedRoute, private dialog: MatDialog, private productService: ProductService,
    private vendorService: VendorService) { }

  ngOnInit(): void {
    this.router.params.subscribe(params => {this.vendorId = params['id']})
    this.reload()
  }

  reload():void{
    this.vendorService.getProducts(this.vendorId).subscribe({
      next: (data: Vendor) =>{
        this.vendor = data
        this.products = data.products
      },
      error: (error: any) =>{
        alert("Failed loading products!" + error.message);
      }
    })
  }

  deleteProduct(id: string):void{
    this.productService.deleteProduct(id).subscribe({
      next: data =>{
        this.reload()
      },
      error: error => {
        alert("Deleting failed! " + error.mess)
      }
    })
  }

  addProduct():void{
    this.dialog.open(AddProductComponent, {data: { productService: this.productService, vendor: this.vendor, dialog: this.dialog}})
    .afterClosed().subscribe(() => this.reload())
  }
}
