import { ReservePlace } from './../../../models/reserve-place.model';
import { Place } from './../../../models/place.model';
import { Vendor } from './../../../models/vendor.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-reserve-dialog',
  templateUrl: './reserve-dialog.component.html',
  styleUrls: ['./reserve-dialog.component.scss']
})
export class ReserveDialogComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { 
  }

  vendors: Vendor[] | null = null;
  selectedVendor: string | null = null;
  reservedPlace: ReservePlace = new ReservePlace;

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

  reservePlace():void{
    if(this.selectedVendor != null){
      this.reservedPlace.vendorId = this.selectedVendor;
      this.reservedPlace.placeId = this.data.clickedPlace.id;
      this.data.placeService.reservePlace(this.reservedPlace).subscribe({
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
