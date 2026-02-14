import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Order } from '../../../contracts/order/create_order';
import { firstValueFrom, Observable } from 'rxjs';
import { List_order } from '../../../contracts/order/list_order';
import { GetByIdOrder } from '../../../contracts/order/getbyid_order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async create(order: Create_Order): Promise<void> {
    const Observable: Observable<any> = this.httpClientService.post({
      controller: "orders"
    }, order)
    await firstValueFrom(Observable);
  }

  async get(page: number = 0, size: number = 5): Promise<List_order> {
    const observable: Observable<any> = this.httpClientService.get({
      controller: "orders",
      queryString: `page=${page}&size=${size}`
    })
    return await firstValueFrom(observable)
  }
  async getOrderById(id: string): Promise<GetByIdOrder> {
    const observable: Observable<GetByIdOrder> = this.httpClientService.get<GetByIdOrder>({
      controller: 'orders'
    }, id)
    const result = await firstValueFrom(observable);
    console.log(result);
    return result;
  }
  async deleteOrder(id : string){
    this.httpClientService.delete({
      controller:"orders"
    },id)
  }
  async completeOrder(id :string){
    const observable :Observable<any>=this.httpClientService.get({
      controller:'orders',
      action:'complete-order'
    },id)
    await firstValueFrom(observable);
  }
}
