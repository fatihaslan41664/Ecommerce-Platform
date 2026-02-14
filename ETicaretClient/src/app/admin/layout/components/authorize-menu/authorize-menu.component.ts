import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BaseComponent, spinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationService } from '../../../../services/common/application.service';
import { CommonModule } from '@angular/common';
import { menuDTO } from '../../../../contracts/authorizeEndPoint/getauthlist';
import { DialogServiceService } from '../../../../services/common/dialog-service.service';
import { AuthorizeMenuDialogComponent } from '../../../../dialogs/authorize-menu-dialog/authorize-menu-dialog.component';

@Component({
  selector: 'app-authorize-menu',
  imports: [MatTreeModule, MatButtonModule, MatIconModule, CommonModule],
  templateUrl: './authorize-menu.component.html',
  styleUrl: './authorize-menu.component.css'
})
export class AuthorizeMenuComponent extends BaseComponent implements OnInit {
  dataSource: MenuNode[] = [];

  constructor(
    spinner: NgxSpinnerService, 
    private applicationService: ApplicationService,
    private dialogService: DialogServiceService
  ) {
    super(spinner);
  }

  async ngOnInit() {
    this.showSpinnerWithoutTimeout(spinnerType.BallAtom);
    try {
      const menus: menuDTO[] = await this.applicationService.getAuthorizeDefinitionEndPoints();
      this.dataSource = this.transformMenuData(menus);
    } catch (error) {
      console.error('Menü yüklenirken hata:', error);
    } finally {
      this.hideSpinner(spinnerType.BallAtom);
    }
  }

  private transformMenuData(menus: menuDTO[]): MenuNode[] {
    return menus.map(menu => ({
      name: menu.menuName,
      menu: menu.menuName, // Menu bilgisini de ekliyoruz
      children: menu.actions.map(action => ({
        name: `${action.definition} (${action.httpType})`,
        code: action.code,
        menu: menu.menuName // Her action'a da parent menu'yu ekliyoruz
      }))
    }));
  }

  childrenAccessor = (node: MenuNode) => node.children ?? [];

  hasChild = (_: number, node: MenuNode) => !!node.children && node.children.length > 0;

  assignRole(code: string, name: string, menu: string) {
    this.dialogService.openDialog({
      componentType: AuthorizeMenuDialogComponent,
      data: { 
        code: code, 
        name: name,
        menu: menu  // Menu bilgisini de gönderiyoruz
      }
    });
  }
}

export interface MenuNode {
  name: string;
  code?: string;
  menu?: string;  // Menu property'sini ekliyoruz
  children?: MenuNode[];
}