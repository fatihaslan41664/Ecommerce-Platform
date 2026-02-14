import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { TokenResponse } from '../../../contracts/token/tokenResponse';
import { HttpClientService } from '../http-client.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private httpClientService: HttpClientService, private toastrService: CustomToastrService) {

  }
  async login(email: string, password: string, callBackFunction?: () => void): Promise<void> {
    const observable: Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: "Auth",
      action: "Login"
    }, { email, password })
    const tokenResponse = await firstValueFrom(observable) as TokenResponse
    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken)
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken)
      this.toastrService.message("giriş basarili", "Basarili", {
        messagetype: ToastrMessageType.Success,
        position: ToastrPosition.Topleft
      })
    }
    callBackFunction();
  }
  async googleLogin(user: SocialUser, callBackFunction?: () => void) {
    const observable: Observable<any> = this.httpClientService.post<SocialUser | TokenResponse>({
      action: "google-login",
      controller: "Auth"
    }, user);
    const tokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse) {

      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken)
      this.toastrService.message("Google ile Giriş", "Giriş Başarılı ", {
        position: ToastrPosition.Topleft,
        messagetype: ToastrMessageType.Success
      })
    }
    callBackFunction();
  }
  async refreshTokenLogin(refreshToken : string, callBackFunction? : ()=> void){
    const observable : Observable<any> = this.httpClientService.post({
      action : "RefreshToken",
      controller:"Auth"
    },{refreshToken:refreshToken});
    const tokenResponse : TokenResponse = await firstValueFrom(observable) as TokenResponse
    if (tokenResponse) {

      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken)
      this.toastrService.message("Sureniz bitti fakat bir sure daha devam edebilirsiniz", "sureniz bitti ", {
        position: ToastrPosition.Topleft,
        messagetype: ToastrMessageType.Warning
      })
    }
    callBackFunction();
  }
  async passwordReset(email:string, callBackFunction? :()=>void){
    const observable : Observable<any> = this.httpClientService.post({
      controller : "Auth",
      action : "password-reset"
    },{ email: email })
    await firstValueFrom(observable);
    callBackFunction?.();
  }
  async verifyResetToken(resetToken: string, userId:string):Promise<boolean>{
    const observable : Observable<any> = this.httpClientService.post({
      controller :"Auth",
      action : "verify-reset-token"
    },{resetToken: resetToken,
      userId : userId
    })
    const state : boolean = await firstValueFrom(observable)
    return state
  }
}
