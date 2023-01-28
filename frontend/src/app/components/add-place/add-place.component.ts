import { AddPlace } from './../../models/place.model';
import { AddVendor } from './../../models/vendor.model';
import { PlaceService } from './../../services/place.service';
import { VendorService } from './../../services/vendor.service';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Vendor } from 'src/app/models/vendor.model';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add-place',
  templateUrl: './add-place.component.html',
  styleUrls: ['./add-place.component.scss']
})
export class AddPlaceComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  vendor: AddVendor = new AddVendor;
  place: AddPlace = new AddPlace;

  ngOnInit(): void {
  }

  addVendor():void{
    this.data.vendorService.addVendor(this.vendor).subscribe({
      next: (data: any) => {
        this.vendor = new AddVendor;
        alert("Added vendor!")
      },
      error: (error: { message: string; }) => {
        alert("Saving vendor failed! " + error.message);
      }
    })
  }

  addPlace():void{
    this.data.placeService.addPlace(this.place).subscribe({
       next: (data: any) => {
        this.place = new AddPlace;
        alert("Added place!")
       },
       error: (error: { message: string; }) => {
        alert("Saving place failed!" + error.message);
       }
    })
  }

  back():void{
    this.data.dialog.closeAll()
  }
}
