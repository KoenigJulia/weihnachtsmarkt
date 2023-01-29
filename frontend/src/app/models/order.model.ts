export interface Order {
    id: string,
    name: string,
    created: Date,
    customerId: string,
    products: string[]
}

export interface AddOrder{
    name: string,
    customerId: string,
    products: string[]
}

export class AddOrder implements AddOrder{
}