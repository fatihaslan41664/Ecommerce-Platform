import { Component } from '@angular/core';
import { BaseComponent } from '../../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { RoleService } from '../../../../../services/common/models/role-service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { CustomToastrService } from '../../../../../services/ui/custom-toastr.service';
import { AlertifyService, MessageType } from '../../../../../services/admin/alertify.service';

@Component({
  selector: 'role-app-create',
  imports: [MatTableModule, MatButtonModule, MatInputModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent extends BaseComponent {

  constructor(spinner: NgxSpinnerService, private roleService: RoleService, private tostrService: CustomToastrService, private alertifyService: AlertifyService) {
    super(spinner);

  }
  async createRole(name: HTMLInputElement) {
    if (!name.value || name.value.trim() === '') {
      this.alertifyService.message('Rol adı boş olamaz', {
        messageType: MessageType.Warning
      });
      return;
    }

    try {
      const result = await this.roleService.createRole(name.value);
      this.alertifyService.message('Rol başarıyla oluşturuldu', {
        messageType: MessageType.Success
      });

      name.value = '';

    } catch (error: any) {
      console.error(error);
      this.alertifyService.message('Rol oluşturulamadı: ' + (error?.message || ''), {
        messageType: MessageType.Error
      });
    }
  }
}
