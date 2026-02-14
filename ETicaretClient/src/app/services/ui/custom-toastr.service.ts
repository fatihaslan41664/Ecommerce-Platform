import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(private toastr: ToastrService) {
  }
  message(message?: string, title?: string, tostrOptions?:Partial<ToastrOptions>) {
    if (tostrOptions.messagetype == ToastrMessageType.Clear) {
      this.toastr.clear();
    }
    else {

      this.toastr[tostrOptions.messagetype](message, title, {
        positionClass: tostrOptions.position
      });
    }
  }
}
export enum ToastrMessageType {
  Success = "success",
  Info = "info",
  Warning = "warning",
  Error = "error",
  Clear = "clear"
}
export enum ToastrPosition {
  Topright = "toast-top-right",
  Topleft = "toast-top-left"
}
export class ToastrOptions{
  messagetype: ToastrMessageType;
  position? : ToastrPosition

}