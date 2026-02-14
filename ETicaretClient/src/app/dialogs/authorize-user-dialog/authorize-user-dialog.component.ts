import { ChangeDetectorRef, Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatListModule, MatSelectionList } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { NgFor, NgIf } from '@angular/common';
import { UserServiceService } from '../../services/common/models/user-service.service';
import { RoleService } from '../../services/common/models/role-service';

@Component({
  selector: 'app-authorize-user-dialog',
  imports: [MatListModule, MatButtonModule, MatDialogModule, NgFor,NgIf],
  templateUrl: './authorize-user-dialog.component.html',
  styleUrl: './authorize-user-dialog.component.css'
})
export class AuthorizeUserDialogComponent implements OnInit {

  roles: { id: string; name: string }[] = [];
  assignedRoles: string[] = [];
  isLoading = true;  // veriler gelene kadar liste render edilmesin
  @ViewChild('roleList') roleList: MatSelectionList;
  constructor(
    public dialogRef: MatDialogRef<AuthorizeUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public userId: string,
    private roleService: RoleService,
    private userService: UserServiceService,
    private cdr: ChangeDetectorRef
  ) {}

  async ngOnInit() {
  const [rolesRes, assignedRes] = await Promise.all([
    this.roleService.read(-1, -1),
    this.userService.getRolesToUser(this.userId)
  ]);
  this.roles = Object.entries(rolesRes.datas).map(([id, name]) => ({ id, name }));
  this.assignedRoles = assignedRes;
  this.isLoading = false;
  this.cdr.detectChanges();
}

  isRoleAssigned(roleName: string): boolean {
    return this.assignedRoles.includes(roleName);
  }

  async assignRoles() {  // ← parametreyi kaldır
  const selectedRoleNames: string[] = this.roleList.selectedOptions.selected
    .map(option => option.value);

  await this.userService.assignRoleUser(
    this.userId,
    selectedRoleNames,
    () => {
      alert('Roller başarıyla atandı!');
      this.dialogRef.close();
    },
    () => alert('Roller atanırken bir hata oluştu!')
  );
}
}