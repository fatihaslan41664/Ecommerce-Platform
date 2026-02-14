import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable,of } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';
import { UserAuthService } from './models/user-auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor{

  constructor(public totstrService : CustomToastrService, private userAuthService : UserAuthService, private router:Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      switch(error.status){
        case HttpStatusCode.Unauthorized:
          const url =this.router.url;
          if(url == "/products"){
            this.totstrService.message("yetkisiz erişim","oturum açın",{
              messagetype:ToastrMessageType.Warning,
              position : ToastrPosition.Topleft
            })
          }
          else{

            this.totstrService.message("Bu işlemi yapmaya yetkiniz yok","yetkisiz erişim",
            {messagetype : ToastrMessageType.Warning, position:ToastrPosition.Topright})
            this.userAuthService.refreshTokenLogin(localStorage.getItem("refreshToken")).then(
              data=>{
                
              }
            );
          }

          break;
        case HttpStatusCode.InternalServerError:
          this.totstrService.message("Sunucuya erişilemiyor","Sunucu hatası",
          {messagetype : ToastrMessageType.Warning, position:ToastrPosition.Topright})
          break;
        case HttpStatusCode.BadRequest:
          this.totstrService.message("Geçersiz istek yapıldı","Geçersiz istek",
          {messagetype : ToastrMessageType.Warning, position:ToastrPosition.Topright})
          break;
        case HttpStatusCode.NotFound:
          this.totstrService.message("Böyle bir sayfa bulunamadı","Sayfa bulunamadı",
          {messagetype : ToastrMessageType.Warning, position:ToastrPosition.Topright})
          break;
        default:
            this.totstrService.message("beklenemyen bir hata meydana geldi","beklenmeyen hata",
          {messagetype : ToastrMessageType.Warning, position:ToastrPosition.Topright})
        break
      }
       console.log(error);
       return of(error)
    }));
  }
}
 