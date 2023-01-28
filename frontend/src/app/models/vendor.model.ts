import { Employee } from './employee.model';
import { Product } from "./product.model";

export interface Vendor {
    id: string,
    name: string,
    products: string[],
    employees: Employee[]
}

export interface AddVendor{
    name: string
}

export class AddVendor implements AddVendor{
}