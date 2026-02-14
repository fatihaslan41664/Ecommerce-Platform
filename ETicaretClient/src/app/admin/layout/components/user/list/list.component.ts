import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { UserServiceService } from '../../../../../services/common/models/user-service.service';
import { MatDialogModule } from '@angular/material/dialog';
import { NgIf } from '@angular/common';
import { DeleteDirective } from '../../../../../directives/admin/delete.directive';
import { kullanan } from '../../../../../contracts/users/get_all_users';
import { MatButtonModule } from '@angular/material/button';
import { DialogServiceService } from '../../../../../services/common/dialog-service.service';
import { AuthorizeUserDialogComponent } from '../../../../../dialogs/authorize-user-dialog/authorize-user-dialog.component';

@Component({
  selector: 'app-list',
  imports: [MatTableModule, MatPaginatorModule, DeleteDirective, MatDialogModule, NgIf, MatButtonModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent implements AfterViewInit {

  displayedColumns: string[] = ['id', 'email', 'nameSurname', 'twoFactorAuthEnabled', 'delete','atama'];
  dataSource = new MatTableDataSource<kullanan>([]);
  totalcount: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private userService: UserServiceService,private dialogService :DialogServiceService) {}

  ngAfterViewInit(): void {
    this.loadUsers();
    this.paginator.page.subscribe(() => this.loadUsers());
  }

  private async loadUsers(): Promise<void> {
    const pageIndex = this.paginator?.pageIndex ?? 0;
    const pageSize  = this.paginator?.pageSize  ?? 5;
    const result = await this.userService.getAllUser(pageIndex, pageSize);
    this.dataSource.data = result.users;
    this.totalcount = result.totalUsersCount;
  }
  assignRole(userId: string) {
  this.dialogService.openDialog({
    componentType: AuthorizeUserDialogComponent,
    data: userId  
  });
}
}