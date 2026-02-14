import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { BaseComponent, spinnerType } from '../../../../../base/base.component';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe, NgIf } from '@angular/common';
import { AlertifyService } from '../../../../../services/admin/alertify.service';
import { DialogServiceService } from '../../../../../services/common/dialog-service.service';
import { SelectProductImageDialogComponent } from '../../../../../dialogs/select-product-image-dialog/select-product-image-dialog.component';
import { DeleteDirective } from '../../../../../directives/admin/delete.directive';
import { MatDialogModule } from '@angular/material/dialog';
import { List_order } from '../../../../../contracts/order/list_order';
import { OrderService } from '../../../../../services/common/models/order.service';
import { OrderDetailDialogComponent, OrderDetailDialogState } from '../../../../../dialogs/order-detail-dialog/order-detail-dialog.component';

@Component({
  selector: 'app-orderlist',
  imports: [MatTableModule, MatPaginatorModule, DeleteDirective, MatDialogModule,DatePipe,NgIf],
  templateUrl: './orderlist.component.html',
  styleUrl: './orderlist.component.css'
})
export class OrderlistComponent extends BaseComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['orderCode', 'userName', 'totalPrice', 'createdDate', 'viewDetail','isCompleted', 'delete'];

  dataSource = new MatTableDataSource<List_order>([]);
  totalcount: number = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    spinner: NgxSpinnerService,
    private orderService: OrderService,
    private alertifyService: AlertifyService,
    private dialogService: DialogServiceService
  ) {
    super(spinner);
  }

  async ngOnInit(): Promise<void> {

  }

  ngAfterViewInit(): void {
    this.loadProducts();
    this.paginator.page.subscribe(() => {
      this.loadProducts();
    });
  }
  addProductImages(id: string) {
    this.dialogService.openDialog(
      {
        componentType: SelectProductImageDialogComponent,
        data: id
      }
    );
  }

  private async loadProducts(): Promise<void> {
    this.showSpinner(spinnerType.BallAtom);
    const pageIndex = this.paginator?.pageIndex || 0;
    const pageSize = this.paginator?.pageSize || 5;
    const result = await this.orderService.get(pageIndex, pageSize);
    this.dataSource.data = result.orders || result;
    this.totalcount = result.totalCount || result.length;
    
    this.hideSpinner(spinnerType.BallAtom);
  }
  showDetail(id){
    this.dialogService.openDialog(
      {
        componentType:OrderDetailDialogComponent,
        data : id
      } 
    )
  }
}
  // addProductImages(id: string) {
  //   this.dialogService.openDialog(
  //     () => {
  //       console.log('Dialog kapandı');
  //     },
  //     {
  //       componentType: SelectProductImageDialogComponent,
  //       data: id
  //     }
  //   );
  // }