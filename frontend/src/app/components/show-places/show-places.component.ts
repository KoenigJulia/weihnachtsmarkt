import { ReserveDialogComponent } from './reserve-dialog/reserve-dialog.component';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Place } from 'src/app/models/place.model';
import { PlaceService } from 'src/app/services/place.service';
import { VendorService } from 'src/app/services/vendor.service';

@Component({
  selector: 'app-show-places',
  templateUrl: './show-places.component.html',
  styleUrls: ['./show-places.component.scss']
})
export class ShowPlacesComponent implements OnInit {

  places: Place[] = [];
  clickedPlace: Place | null = null;

  constructor(private placeService: PlaceService, private vendorService: VendorService, 
    private router: Router, private dialog: MatDialog){}

  ngOnInit(): void{
    this.placeService.getAll().subscribe({
      next: data => {
        this.places = data;
      },
      error: error =>{
        alert ("Error while loading places! " + error.message);
      }
    });   
  }

  reservePlace():void{
    this.dialog.open(ReserveDialogComponent, {data: { clickedPlace: this.clickedPlace, vendorService: this.vendorService, placeService: this.placeService, dialog: this.dialog}})
  }

  getVendor(vendorId: string): any{
    this.vendorService.getVendorById(vendorId).subscribe({
      next: data => {
        return data;
      },
      error: error =>{
        alert ("No vendor with this id " + error.message);
      }
    })
  }

  addPlace():void{
    this.router.navigate(['/addPlace'])
  }

}
