import { Component, Inject, OnInit } from '@angular/core';
import { BaseDialogs } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule, } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { OrderService } from '../../services/common/models/order.service';
import { MatToolbarModule } from '@angular/material/toolbar';
import { GetByIdOrder } from '../../contracts/order/getbyid_order';
import { CommonModule, NgIf } from '@angular/common';
import { DialogServiceService } from '../../services/common/dialog-service.service';
import { CompleteOrderDialogComponent, CompleteOrderState } from '../complete-order-dialog/complete-order-dialog.component';

@Component({
  selector: 'app-order-detail-dialog',
  imports: [MatDialogModule,MatButtonModule,MatTableModule,CommonModule,MatToolbarModule,NgIf],
  templateUrl: './order-detail-dialog.component.html',
  styleUrl: './order-detail-dialog.component.css'
})
export class OrderDetailDialogComponent extends BaseDialogs<OrderDetailDialogComponent> implements OnInit{
  getByIdOrder:GetByIdOrder
  OrderDetailDialogState = OrderDetailDialogState;
  constructor(dialogRef: MatDialogRef<OrderDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrderDetailDialogComponent | string,
    private orderService : OrderService,
    private dialogService :DialogServiceService
  ) {
    
    super(dialogRef)
  }
AllPrice:number
async ngOnInit(): Promise<void>{
  // Burada sonucu alıp getByIdOrder'a ataman lazım
  this.getByIdOrder = await this.orderService.getOrderById(this.data as string);
  this.dataSource = this.getByIdOrder.basketItems;
  this.AllPrice = this.getByIdOrder.basketItems.map((basketItem,index)=>basketItem.price * basketItem.quantity).reduce((price,current)=> price+current)
}
    displayedColumns: string[] = ['name', 'price', 'quantity', 'totalPrice'];
    dataSource = [];
    clickedRows = new Set<any>();
    completeOrder(){
      this.dialogService.openDialog({
        componentType:CompleteOrderDialogComponent,
        data: this.data as string
      })
    }
}

export enum OrderDetailDialogState{
  Close, OrderComplate
}
    // address : string
    // basketItems 
    // createdDate :Date
    // Description : string
    // id : string
    // orderCode : string