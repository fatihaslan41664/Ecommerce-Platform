import { Component } from '@angular/core';
import { BaseComponent, spinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClientService } from '../../../../services/common/http-client.service';
import { Create_product } from '../../../../contracts/create_product';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-product',
  imports: [MatSidenavModule, CreateComponent, ListComponent, MatTableModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
  
}
