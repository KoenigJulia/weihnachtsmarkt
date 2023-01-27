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

@NgModule({
  declarations: [
    AppComponent,
    ReserveDialogComponent,
    ShowPlacesComponent,
    AddPlaceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NoopAnimationsModule,
    MatDialogModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
