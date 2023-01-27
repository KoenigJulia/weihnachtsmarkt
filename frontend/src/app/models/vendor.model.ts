import { Employee } from './employee.model';
import { Product } from "./product.model";

export interface Vendor {
    id: string,
    name: string,
    products: Product[],
    employees: Employee[]
}
