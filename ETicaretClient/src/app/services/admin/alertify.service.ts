import { Injectable } from '@angular/core';
import alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }
  message(message: string, option: Partial<alertifyOptions>) {
    if (option.position)
      alertify.set('notifier', 'position', option.position);

    if (option.delay !== undefined)
      alertify.set('notifier', 'delay', option.delay);

    const msj = alertify[option.messageType || 'message'](message);

    if (option.dismissOthers) {
      msj.dismissOthers();
    }
  }
  dismiss() {
    alertify.dismissAll();
  }
}
export enum MessageType {
  Error = "error",
  Message = "message",
  Notify = "notify",
  Success = "success",
  Warning = "warning"
}
export enum Position {
  TopCenter = "top-center",
  TopRight = "top-right",
  TopLeft = "top-left",
  BottomRight = "bottom-right",
  BottomCenter = "bottom-center",
  BottomLeft = "bottom-left"
}
export class alertifyOptions {
  messageType: MessageType = MessageType.Message;
  position: Position = Position.BottomLeft;
  delay: number = 3;
  dismissOthers: boolean = false;

}