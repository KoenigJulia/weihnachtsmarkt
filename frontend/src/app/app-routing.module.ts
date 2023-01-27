import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPlaceComponent } from './components/add-place/add-place.component';
import { ShowPlacesComponent } from './components/show-places/show-places.component';

const routes: Routes = [
  {path: '', redirectTo:'showPlaces', pathMatch: 'full'},
  {path: 'showPlaces', component: ShowPlacesComponent},
  {path: 'addPlace', component: AddPlaceComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
