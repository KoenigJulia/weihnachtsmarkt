import { AddPlace } from './../models/place.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Place, ReservePlace } from '../models/place.model';

const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class PlaceService {
  placeUrl: string = "http://the-purge.at:9418/api/Place/"

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<Place[]>(this.placeUrl + "all");
  }

  reservePlace(newPlace: ReservePlace){
    return this.http.post<ReservePlace>(this.placeUrl+"reserve", newPlace, httpOptions)
  }

  deletePlace(id: string){
    return this.http.delete<Place>(this.placeUrl + "?id=" + id, httpOptions)
  }

  addPlace(newPlace: AddPlace){
    return this.http.post<AddPlace>(this.placeUrl+ "place", newPlace, httpOptions);
  }

}
