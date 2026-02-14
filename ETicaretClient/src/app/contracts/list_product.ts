import { List_product_Image } from "./list_product_images";

export class List_product {
    id: string;
    category: string;
    price: number;
    stock: number;
    color: string;
    name: string;
    series: string;
    productImages?: List_product_Image[];
    imagePath?:string;
}

