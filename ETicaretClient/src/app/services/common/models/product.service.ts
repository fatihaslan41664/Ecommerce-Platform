import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_product } from '../../../contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_product } from '../../../contracts/list_product';
import { firstValueFrom, Observable } from 'rxjs';
import { List_product_Image } from '../../../contracts/list_product_images';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(
    product: Create_product,
    successCallBack?: () => void,
    errorCallBack?: (message: string) => void
  ): void {
    this.httpClientService.post({ controller: "product" }, product).subscribe({
      next: () => {
        if (successCallBack) successCallBack();
      },
      error: (errorResponse: HttpErrorResponse) => {
        const error = errorResponse.error;
        let message = "";
        if (Array.isArray(error)) {
          error.forEach((e: any) => {
            if (Array.isArray(e.messages)) {
              e.messages.forEach((msg: string) => {
                message += `${msg}<br>`;
              });
            }
          });
        }
        else if (error && typeof error === 'object') {
          Object.values(error).forEach((msgs: any) => {
            if (Array.isArray(msgs)) {
              msgs.forEach((msg: string) => {
                message += `${msg}<br>`;
              });
            }
          });
        }
        else {
          message = "Beklenmeyen bir hata oluştu.";
        }

        if (errorCallBack) errorCallBack(message);
      }
    });
  }
  async read(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ products: List_product[], totalcount: number }> {
    const promiseData: Promise<{ products: List_product[], totalcount: number }> = this.httpClientService.get<{ products: List_product[], totalcount: number }>({
      controller: 'product',
      queryString: `page=${page}&size=${size}`
    }).toPromise();
    promiseData.then(d => successCallBack())
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))
    return await promiseData;
  }
  async delete(id: string) {
    const deleteObservable: Observable<any> = this.httpClientService.delete<any>({
      controller: "product"
    }, id);
    await firstValueFrom(deleteObservable);
  }
  async readImages(id: string): Promise<List_product_Image[]> {
    const getObsaervable: Observable<List_product_Image[]> = this.httpClientService.get<List_product_Image[]>({
      action: "GetProductImages",
      controller: "product"
    }, id)
    return await firstValueFrom(getObsaervable);
  }
  async deleteİmage(id: string, imageId: string) {
    const deleteObservable = this.httpClientService.delete({
      action: "DeleteProductImage",
      controller: "product",
      queryString: `imageId=${imageId}`
    }, id)
    await firstValueFrom(deleteObservable);
  }
  async changeShowImage(imageId : string , productId:string, successCallBack? : ()=>void):Promise<void>{
    const asd = this.httpClientService.put({
      controller:"product",
      action:"ChangeShowCase",
      queryString:`imageId=${imageId}&productId=${productId}`
    })
    await firstValueFrom(asd);
    successCallBack()
  }
}
