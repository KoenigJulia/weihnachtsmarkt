import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Vendor } from '../models/vendor.model';

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  placeUrl: string = "http://localhost:5000/api/Vendor/"

  constructor(private http: HttpClient) { }

  getVendorById(vendorId: string){
    return this.http.get<Vendor>(this.placeUrl + "vendor?vendorId="+vendorId);
  }

  getAll(){
    return this.http.get<Vendor[]>(this.placeUrl + "all");
  }
}
