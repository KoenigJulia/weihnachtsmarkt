export interface Product {
    id: string,
    name: string,
    price: number,
    vendorId: string
}

export interface AddProductToOrder{
    productId: string
}

export class AddProductToOrder implements AddProductToOrder{
}

export interface AddProduct{
    name: string,
    price: number,
    vendorId: string
}

export class AddProduct implements AddProduct{
}