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

  allProducts: Product[] = []
  products: Product[] = []
  vendorId: string = "";
  vendor: Vendor | null = null;
  clickedProduct: Product | null = null;

  constructor(private route: ActivatedRoute, private router: Router, private dialog: MatDialog, private productService: ProductService,
    private vendorService: VendorService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => { this.vendorId = params['id'] })
    this.reloadVendor()
  }

  reloadVendor():void{
    this.vendorService.getProducts(this.vendorId).subscribe({
      next: (data: Vendor) => {
        this.vendor = data
        this.reloadProducts()
      },
      error: (error: any) => {
        alert("Failed loading vendor!" + error.message);
      }
    })
  }

  reloadProducts(): void {
    this.products = []
    this.allProducts = []
    this.productService.getAll().subscribe({
      next: (data: Product[]) => {
        this.allProducts = data
        this.vendor!.products.forEach(element => {
          if (this.allProducts.find(p => p.id == element)) {
            this.products.push(this.allProducts.find(p => p.id == element)!)
          }
        })
      },
      error: (error: any) => {
        alert("Failed loading products!" + error.message);
      }
    })
  }

  deleteProduct(id: string): void {
    this.productService.deleteProduct(id).subscribe({
      next: data => {
        this.reloadVendor()
      },
      error: error => {
        alert("Deleting failed! " + error.mess)
      }
    })
  }

  addProduct(): void {
    this.dialog.open(AddProductComponent, { data: { productService: this.productService, vendor: this.vendor, dialog: this.dialog } })
      .afterClosed().subscribe(() => this.reloadVendor())
  }

  back(): void {
    this.router.navigate(['/showPlaces']);
  }
}
