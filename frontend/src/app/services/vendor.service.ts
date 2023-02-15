import { Employee } from './../models/employee.model';
import { AddVendor } from './../models/vendor.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Vendor } from '../models/vendor.model';

const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class VendorService {

  placeUrl: string = "http://the-purge.at:9418/api/Vendor/"

  constructor(private http: HttpClient) { }

  getAllEmployees(){
    return this.http.get<Vendor>(this.placeUrl + "employee/all");
  }

  getVendorById(vendorId: string){
    return this.http.get<Vendor>(this.placeUrl + "vendor?vendorId="+vendorId);
  }

  getAll(){
    return this.http.get<Vendor[]>(this.placeUrl + "all");
  }

  addVendor(vendor: AddVendor){
    return this.http.post<AddVendor>(this.placeUrl+ "vendor", vendor, httpOptions);
  }

  deleteVendor(id: string){
    return this.http.delete<Vendor>(this.placeUrl + "?id=" + id, httpOptions)
  }

  getProducts(id: string){
    return this.http.get<Vendor>(this.placeUrl + "vendor?vendorId="+id);
  }

  addEmployee(newEmployee: Employee, vendorId: string){
    return this.http.post<Employee>(this.placeUrl+ "vendor/" + vendorId + "/employee", newEmployee, httpOptions);
  }
}
