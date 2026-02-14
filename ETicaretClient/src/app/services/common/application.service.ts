import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { firstValueFrom, Observable } from 'rxjs';
import { menuDTO } from '../../contracts/authorizeEndPoint/getauthlist';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private httpClientService : HttpClientService) { }
  async getAuthorizeDefinitionEndPoints(){
    const observable :Observable<menuDTO[]> = this.httpClientService.get({
      controller :"ApplicationService"
    })
    return await firstValueFrom(observable);
  }
}
