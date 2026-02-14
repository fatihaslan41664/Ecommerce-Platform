import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom, Observable } from 'rxjs';
import { List_Basket_Item } from '../../../contracts/basket/list_basket_item';
import { Create_Basket_Item } from '../../../contracts/basket/create_basket_item';
import { Update_Quantity } from '../../../contracts/basket/update_quantity';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private httpClientService: HttpClientService) { }
  async getBasket(): Promise<List_Basket_Item[]> {
    const observble: Observable<List_Basket_Item[]> = this.httpClientService.get({
      controller: "Basket"
    })
    return await firstValueFrom(observble)
  }
  async addBasket(item: Create_Basket_Item): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "Basket",

    }, item)
    return await firstValueFrom(observable)
  }
  async updateQuantity(basketItem: Update_Quantity): Promise<void> {
    const observable: Observable<any> = this.httpClientService.put({
      controller: "Basket"
    }, basketItem)
    await firstValueFrom(observable)
  }
  async removeBasketItem(basketItemId: string) {
    const observable: Observable<any> = this.httpClientService.delete({
      controller: "Basket",
    }, basketItemId)
    await firstValueFrom(observable);
  }
}
