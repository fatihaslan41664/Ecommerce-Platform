import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { List_product } from '../../../../../contracts/list_product';
import { BaseComponent, spinnerType } from '../../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ProductService } from '../../../../../services/common/models/product.service';
import { AlertifyService, MessageType } from '../../../../../services/admin/alertify.service';
import { CurrencyPipe } from '@angular/common';
import { DeleteDirective } from '../../../../../directives/admin/delete.directive';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogServiceService } from '../../../../../services/common/dialog-service.service';
import { SelectProductImageDialogComponent } from '../../../../../dialogs/select-product-image-dialog/select-product-image-dialog.component';

@Component({
  selector: 'app-list',
  imports: [MatTableModule, MatPaginatorModule, CurrencyPipe, DeleteDirective, MatDialogModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent extends BaseComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['name', 'series', 'category', 'price', 'stock', 'color', 'photos', 'edit', 'delete'];

  dataSource = new MatTableDataSource<List_product>([]);
  totalcount: number = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    spinner: NgxSpinnerService,
    private productservice: ProductService,
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

    try {
      const response: any = await this.productservice.read(
        pageIndex,
        pageSize,
        () => this.hideSpinner(spinnerType.BallAtom),
        (errorMessage: string) => {
          this.alertifyService.message(errorMessage, {
            dismissOthers: true,
            messageType: MessageType.Error,
          });
          this.hideSpinner(spinnerType.BallAtom);
        }
      );
      if (response && response.products && response.totalcount !== undefined) {
        this.dataSource.data = response.products;
        this.totalcount = response.totalcount;
      } else {
        this.dataSource.data = response;
        this.totalcount = response.length;
      }

    } catch (error) {
      this.hideSpinner(spinnerType.BallAtom);
      this.alertifyService.message('Ürünler yüklenirken hata oluştu', {
        dismissOthers: true,
        messageType: MessageType.Error,
      });
    }
  }
}

// export interface PeriodicElement {
//   name: string;
//   position: number;
//   weight: number;
//   symbol: string;
// }