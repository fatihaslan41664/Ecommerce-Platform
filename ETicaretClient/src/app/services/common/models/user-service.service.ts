import { Injectable } from '@angular/core';

import { User } from '../../../entities/user';
import { HttpClientService } from '../http-client.service';
import { Create_User } from '../../../contracts/users/create_user';
import { defaultIfEmpty, firstValueFrom, Observable } from 'rxjs';
import { Token } from '../../../contracts/token/token';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { TokenResponse } from '../../../contracts/token/tokenResponse';
import { SocialUser } from '@abacritt/angularx-social-login';
import { get_all_users } from '../../../contracts/users/get_all_users';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) {

  }

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller: "Users",
      action: "Create"
    }, user)
    return await firstValueFrom(observable) as Create_User;
  }
  async forgetPasswordUpdate(userId: string, resetToken: string, newpassword: string, passwordConfirm: string) {
    const observable: Observable<any> = this.httpClientService.post({
      action: "forget-password-update",
      controller: "users"
    }, {
      userId: userId,
      resetToken: resetToken,
      newpassword: newpassword,
      passwordConfirm: passwordConfirm
    })
    await firstValueFrom(observable);
  }
  async getAllUser(page: number = 0, size: number = 5) {
    const observable: Observable<get_all_users> = this.httpClientService.get({
      controller: "users",
      queryString: `page=${page}&size=${size}`
    })
    return await firstValueFrom(observable)
  }
  async assignRoleUser(id: string, roles: string[], successCallBack?: () => void, errorCallBack?: () => void) {
    const observable: Observable<any> = this.httpClientService.post({
      controller: 'users',
      action: 'assign-role-user'
    }, {
      userId: id,
      roles: roles
    });

    const data = firstValueFrom(observable.pipe(defaultIfEmpty(null))); // ← boş gelirse null
    data.then(() => successCallBack?.())
      .catch(() => errorCallBack?.());
    return await data;
  }
  async getRolesToUser(userId: string) {
  const observable: Observable<{ userRoles: string[] }> = this.httpClientService.get({
    controller: 'users',
    action: `get-roles-to-user/${userId}`
  });

  try {
    const data = await firstValueFrom(observable.pipe(defaultIfEmpty({ userRoles: [] })));
    return data?.userRoles ?? [];  // ← undefined gelirse boş array dön
  } catch {
    return [];  // ← hata olursa da boş array dön, dialog açılsın
  }
}

}
