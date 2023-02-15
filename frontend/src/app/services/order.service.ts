import { AddProductToOrder, Product } from './../models/product.model';
import { AddOrder, Order } from './../models/order.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer, AddCustomer } from '../models/customer.model';

const httpOptions = {
  headers: new HttpHeaders({
  'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  placeUrl: string = "http://the-purge.at:9418/api/Order/"

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<Order[]>(this.placeUrl + "all");
  }

  getOrderById(id: string){
    return this.http.get<Order>(this.placeUrl + "order?orderId=" + id)
  }

  getOrderPrice(id: string){
    return this.http.get<number>(this.placeUrl + "price?orderId=" + id)
  }

  deleteOrder(id: string){
    return this.http.delete<Order>(this.placeUrl + "order?orderId=" + id, httpOptions)
  }

  addOrder(newOrder: AddOrder){
    return this.http.post<AddOrder>(this.placeUrl+ "order", newOrder, httpOptions);
  }
  
  getProductsByOrderId(id: string){
    return this.http.get<Product[]>(this.placeUrl + "order/" + id + "/products", httpOptions)
  }

  addProductToOrder(id: string, product: AddProductToOrder){
    return this.http.post<AddOrder>(this.placeUrl+ "order/" + id + "/product", product, httpOptions);
  }

  deleteProduct(orderId: string, productId: string){
    return this.http.delete<Order>(this.placeUrl + "order/" + orderId + "/product/" + productId, httpOptions)
  }
}