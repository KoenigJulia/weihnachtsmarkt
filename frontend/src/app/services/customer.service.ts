import { Customer, AddCustomer } from './../models/customer.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Place, ReservePlace, AddPlace } from '../models/place.model';

const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  placeUrl: string = "http://backend:5000/api/Customer/"

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<Customer[]>(this.placeUrl + "all");
  }

  getCustomerById(id: string){
    return this.http.get<Customer>(this.placeUrl + "customer?customerId=" + id)
  }

  deleteCustomer(id: string){
    return this.http.delete<Customer>(this.placeUrl + "?id=" + id, httpOptions)
  }

  addCustomer(newCustomer: AddCustomer){
    return this.http.post<AddCustomer>(this.placeUrl+ "customer", newCustomer, httpOptions);
  }
}
