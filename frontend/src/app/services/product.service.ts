import { AddProduct } from './../models/product.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';

const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  placeUrl: string = "http://the-purge.at:9418/api/Product/"

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<Product[]>(this.placeUrl + "all");
  }

  deleteProduct(id: string){
    return this.http.delete<Product>(this.placeUrl + "?id=" + id, httpOptions)
  }

  addProduct(newProduct: AddProduct){
    return this.http.post<AddProduct>(this.placeUrl+ "product", newProduct, httpOptions);
  }
}
