export interface Product {
    id: string,
    name: string,
    price: number,
    vendorId: string
}

export interface AddProduct{
    name: string,
    price: number,
    vendorId: string
}

export class AddProduct implements AddProduct{
}