import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { BaseComponent, spinnerType } from '../../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { RoleService } from '../../../../../services/common/models/role-service';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { AlertifyService, MessageType } from '../../../../../services/admin/alertify.service';
import { CommonModule } from '@angular/common';
import { DeleteDirective } from '../../../../../directives/admin/delete.directive';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'role-app-list',
  imports: [MatTableModule, MatPaginatorModule, CommonModule, DeleteDirective],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent extends BaseComponent implements OnInit, AfterViewInit {
  constructor(
    spinnerService: NgxSpinnerService, 
    private roleService: RoleService, 
    private alertifyService: AlertifyService
  ) {
    super(spinnerService)
  }

  displayedColumns: string[] = ['name', 'edit', 'delete']
  dataSource = new MatTableDataSource<RoleItem>([]);
  totalcount: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngOnInit(): void {
    this.loadProducts();
  }

  // ✅ Paginator'ı bağla
  ngAfterViewInit(): void {
    // Paginator olaylarını dinle
    this.paginator.page
      .pipe(
        tap(() => this.loadProducts())
      )
      .subscribe();
  }

  private async loadProducts(): Promise<void> {
    this.showSpinner(spinnerType.BallAtom);

    const pageIndex = this.paginator?.pageIndex || 0;
    const pageSize = this.paginator?.pageSize || 5;

    try {
      const response = await this.roleService.read(
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

      if (response && response.datas) {
        const rolesArray: RoleItem[] = Object.entries(response.datas).map(([id, name]) => ({
          id,
          name
        }));
        
        this.dataSource.data = rolesArray;
        this.totalcount = response.totalRoleCount;
      } else {
        this.dataSource.data = [];
        this.totalcount = 0;
      }

    } catch (error) {
      this.hideSpinner(spinnerType.BallAtom);
      this.alertifyService.message('Roller yüklenirken hata oluştu', {
        dismissOthers: true,
        messageType: MessageType.Error,
      });
    }
  }
}

interface RoleItem {
  id: string;
  name: string;
}