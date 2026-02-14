import { Component, OnInit } from '@angular/core';
import { AlertifyService, MessageType, Position } from '../../../../services/admin/alertify.service';
import { SignalRService } from '../../../../services/common/signal-r.service';
import { HubUrls } from '../../../../constants/hub-urls';
import { ReviceFunction } from '../../../../constants/revice-function'

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  constructor(private alertify: AlertifyService, private signalRService: SignalRService) {
    signalRService.start(HubUrls.ProductHub)
    signalRService.start(HubUrls.OrderHub)
  }
  ngOnInit(): void {
    this.signalRService.on(ReviceFunction.ProdcutAddedMessageReciveFunction, message => {
      this.alertify.message(message, { messageType: MessageType.Success, delay: 3, dismissOthers: false, position: Position.TopCenter })
    });
        this.signalRService.on(ReviceFunction.OrdertAddedMessageReciveFunction, message => {
      this.alertify.message(message, { messageType: MessageType.Success, delay: 3, dismissOthers: false, position: Position.TopLeft })
    })
  }
  click() {
    this.alertify.message("Merhaba", { messageType: MessageType.Success, delay: 3, dismissOthers: false, position: Position.TopCenter })
  }
  dissmis() {
    this.alertify.dismiss()
  }
}
