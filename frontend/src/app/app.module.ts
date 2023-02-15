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
import { AddProductComponent } from './components/show-products/add-product/add-product.component';
import { CustomerLoginComponent } from './components/customer-login/customer-login.component';
import { ShowCustomerComponent } from './components/show-customer/show-customer.component';
import { AddOrderComponent } from './components/add-order/add-order.component';
import { ShowOrderProductsComponent } from './components/show-customer/show-order-products/show-order-products.component';
import { AddProductOrderComponent } from './components/show-customer/add-product-order/add-product-order.component';
import { ShowEmployeesComponent } from './components/show-places/show-employees/show-employees.component';
import { AddEmployeeComponent } from './components/show-places/show-employees/add-employee/add-employee.component';
import { ShowAllEmployeesComponent } from './components/show-places/show-all-employees/show-all-employees.component';

@NgModule({
  declarations: [
    AppComponent,
    ReserveDialogComponent,
    ShowPlacesComponent,
    AddPlaceComponent,
    DeleteVendorComponent,
    ShowProductsComponent,
    AddProductComponent,
    CustomerLoginComponent,
    ShowCustomerComponent,
    AddOrderComponent,
    ShowOrderProductsComponent,
    AddProductOrderComponent,
    ShowEmployeesComponent,
    AddEmployeeComponent,
    ShowAllEmployeesComponent
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
