import { ReserveDialogComponent } from './reserve-dialog/reserve-dialog.component';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Place } from 'src/app/models/place.model';
import { PlaceService } from 'src/app/services/place.service';
import { VendorService } from 'src/app/services/vendor.service';
import { Vendor } from 'src/app/models/vendor.model';

@Component({
  selector: 'app-show-places',
  templateUrl: './show-places.component.html',
  styleUrls: ['./show-places.component.scss']
})
export class ShowPlacesComponent implements OnInit {

  places: Place[] = [];
  clickedPlace: Place | null = null;
  vendors: Vendor[] = [];

  constructor(private placeService: PlaceService, private vendorService: VendorService, 
    private router: Router, private dialog: MatDialog){}

  ngOnInit(): void{
    this.reload()
  }

  reload():void{
    this.placeService.getAll().subscribe({
      next: data => {
        this.places = data;
      },
      error: error =>{
        alert ("Error while loading places! " + error.message);
      }
    });   
    this.vendorService.getAll().subscribe({
      next: data => {
        this.vendors = data;
      },
      error: error =>{
        alert ("No vendor with this id " + error.message);
      }
    })
  }

  reservePlace():void{
    this.dialog.open(ReserveDialogComponent, {data: { clickedPlace: this.clickedPlace, vendorService: this.vendorService, placeService: this.placeService, dialog: this.dialog}})
    .afterClosed().subscribe(() => this.reload())
    
  }

  getVendor(vendorId: string): any{
    var name = this.vendors.find(v => v.id == vendorId)?.name
    return name
  }

  addPlace():void{
    this.router.navigate(['/addPlace'])
  }

}
