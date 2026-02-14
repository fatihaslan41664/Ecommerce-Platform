import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationEndpointService {

  constructor(private httpClientService: HttpClientService) { }

  async assignRoleEndPoint(
    roleIds: string[],
    code: string, 
    menu: string
  ) {
    const observable: Observable<any> = this.httpClientService.post({
      controller: 'AuthorizationEndPoints',
    }, {
      roleIds: roleIds,
      endPointCode: code,
      menu: menu
    });

    const response = await firstValueFrom(observable);
    console.log('Backend response:', response);
    return response;
  }
  async getRolestoEndpoint(code :string, menu:string){
    const observable: Observable<any> = this.httpClientService.post({
      controller:'AuthorizationEndPoints',
      action:'GetRolesEndPoint'
    },{code:code,menu:menu})
    observable.subscribe(o=>console.log(o))
    return await firstValueFrom(observable);
  }
  
}