import { ShowCustomerComponent } from './components/show-customer/show-customer.component';
import { CustomerLoginComponent } from './components/customer-login/customer-login.component';
import { ShowProductsComponent } from './components/show-products/show-products.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowPlacesComponent } from './components/show-places/show-places.component';

const routes: Routes = [
  {path: '', redirectTo:'showPlaces', pathMatch: 'full'},
  {path: 'showPlaces', component: ShowPlacesComponent},
  {path: 'showProducts/:id', component: ShowProductsComponent},
  {path: 'loginCustomer', component: CustomerLoginComponent},
  {path: 'showCustomer/:id', component: ShowCustomerComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
