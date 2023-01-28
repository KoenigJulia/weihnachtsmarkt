export interface Place {
    id: string;
    placeNr: number;
    vendorId: string;
}

export interface AddPlace{
    placeNr: number
}

export class AddPlace implements AddPlace{
}

export interface ReservePlace {
    vendorId: string,
    placeId: string
}

export class ReservePlace implements ReservePlace{
}