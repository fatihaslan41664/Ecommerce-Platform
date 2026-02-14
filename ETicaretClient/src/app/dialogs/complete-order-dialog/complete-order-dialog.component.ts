import { Component, Inject } from '@angular/core';
import { BaseDialogs } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { OrderService } from '../../services/common/models/order.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-complete-order-dialog',
  imports: [MatDialogModule,MatButtonModule],
  templateUrl: './complete-order-dialog.component.html',
  styleUrl: './complete-order-dialog.component.css'
})
export class CompleteOrderDialogComponent extends BaseDialogs<CompleteOrderDialogComponent> {

  constructor(dialogRef: MatDialogRef<CompleteOrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CompleteOrderState | string,private orderService : OrderService
    ,private tostrService : CustomToastrService
  ){

    super(dialogRef)
  }
  complete(){
    this.orderService.completeOrder(this.data as string).then(()=>{
      this.tostrService.message("Sipariş Durumu","Sipariş durumu başarıyla oluşturuldu",{
        messagetype: ToastrMessageType.Success,
        position : ToastrPosition.Topright
      })
    }
    ).catch((err)=>{
      this.tostrService.message("hata alındı",err,{
        messagetype: ToastrMessageType.Error,
        position : ToastrPosition.Topright
      })
    })
  }
}
export enum CompleteOrderState {
  Yes,
  No
}
