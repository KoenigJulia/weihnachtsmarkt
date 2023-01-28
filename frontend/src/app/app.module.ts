import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReserveDialogComponent } from './components/show-places/reserve-dialog/reserve-dialog.component';
import { ShowPlacesComponent } from './components/show-places/show-places.component';
import { AddPlaceComponent } from './components/add-place/add-place.component';
import { MatDialogModule } from '../../node_modules/@angular/material/dialog'
import {MatSelectModule} from '@angular/material/select';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { DeleteVendorComponent } from './components/delete-vendor/delete-vendor.component';
import { ShowProductsComponent } from './components/show-products/show-products.component';

@NgModule({
  declarations: [
    AppComponent,
    ReserveDialogComponent,
    ShowPlacesComponent,
    AddPlaceComponent,
    DeleteVendorComponent,
    ShowProductsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatDialogModule,
    MatSelectModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
