import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Vendor } from 'src/app/models/vendor.model';

@Component({
  selector: 'app-delete-vendor',
  templateUrl: './delete-vendor.component.html',
  styleUrls: ['./delete-vendor.component.scss']
})
export class DeleteVendorComponent implements OnInit {
  vendors: Vendor[] | null = null;
  selectedVendor: string | null = null;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
    this.data.vendorService.getAll().subscribe({
      next: (data: Vendor[] | null) => {
        this.vendors = data
      },
      error: (error: { message: string; }) => {
      alert ("Error while loading vendors! " + error.message);
      }
    });
  }

  deleteVendor():void{
    if(this.selectedVendor != null){
      this.data.vendorService.deleteVendor(this.selectedVendor).subscribe({
        next: (data: any) =>{
          this.data.dialog.closeAll()
        },
        error: (error: { message: string; }) => {
          alert('Saving failed! ' + error.message);
        }
      });
    } 
  }
}
