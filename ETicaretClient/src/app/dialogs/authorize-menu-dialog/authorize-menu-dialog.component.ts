import { ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { BaseDialogs } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatListModule, MatSelectionList } from '@angular/material/list';
import { RoleService } from '../../services/common/models/role-service';
import { AuthorizationEndpointService } from '../../services/common/models/authorization-endpoint.service';
import { MatIconModule } from '@angular/material/icon';


@Component({
  selector: 'app-authorize-menu-dialog',
  imports: [MatDialogModule, MatButtonModule, MatBadgeModule, MatListModule,MatIconModule],
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrl: './authorize-menu-dialog.component.css'
})
export class AuthorizeMenuDialogComponent extends BaseDialogs<AuthorizeMenuDialogComponent> implements OnInit {
  roles: tutuchu[] = [];
  assignedRoles: string[] = []; // Atanmış rol isimleri
  
  constructor(
    dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AuthorizationDialogData,
    private roleService: RoleService,
    private authservice: AuthorizationEndpointService,
    private cdr: ChangeDetectorRef
  ) {
    super(dialogRef);
  }

  async ngOnInit() {
    // Tüm rolleri al
    const res = await this.roleService.read(-1, -1);
    this.roles = Object.entries(res.datas).map(([id, name]) => ({
      id,
      name
    }));
    
    // Bu endpoint'e atanmış rolleri al
    const response = await this.authservice.getRolestoEndpoint(this.data.code, this.data.menu);
    this.assignedRoles = response.roles || [];
    
    this.cdr.detectChanges();
  }

  // Rolün seçili olup olmayacağını kontrol et
  isRoleAssigned(roleName: string): boolean {
    return this.assignedRoles.includes(roleName);
  }

  async assignRoles(roleList: MatSelectionList) {
    const selectedRoleIds: string[] = roleList.selectedOptions.selected.map(option => option.value);
    
    try {
      await this.authservice.assignRoleEndPoint(
        selectedRoleIds,
        this.data.code,
        this.data.menu
      );
      
      alert('Roller başarıyla atandı!');
      this.dialogRef.close(AuthorizeMenuState.Yes);
      
    } catch (error) {
      console.error('Backend hatası:', error);
      alert('Roller atanırken bir hata oluştu!');
    }
  }
}

export enum AuthorizeMenuState {
  Yes,
  No
}

export interface AuthorizationDialogData {
  name: string;
  code: string;
  menu: string;
}

export class definer {
  name: string;
  code: string;
}

export class tutuchu {
  id: string;
  name: string;
}