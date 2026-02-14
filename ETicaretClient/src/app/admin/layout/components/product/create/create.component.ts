import { Component, Output, ViewChild } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ProductService } from '../../../../../services/common/models/product.service';
import { Create_product } from '../../../../../contracts/create_product';
import { BaseComponent, spinnerType } from '../../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../../../../services/admin/alertify.service';


@Component({
  selector: 'app-create',
  imports: [MatInputModule, MatFormFieldModule, MatButtonModule],
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent extends BaseComponent {

  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AlertifyService
  ) {
    super(spinner)
  }


  create(
    name: HTMLInputElement,
    series: HTMLInputElement,
    category: HTMLInputElement,
    price: HTMLInputElement,
    stock: HTMLInputElement,
    height: HTMLInputElement,
    color: HTMLInputElement,
    description: HTMLTextAreaElement,

  ) {
    this.showSpinner(spinnerType.BallAtom);

    const create_product: Create_product = {
      name: name.value,
      series: series.value,
      category: category.value,
      price: parseFloat(price.value),
      stock: parseInt(stock.value),
      height: parseFloat(height.value),
      color: color.value,
      description: description.value,

    };

    this.productService.create(create_product, () => {
      this.hideSpinner(spinnerType.BallAtom);
      this.alertifyService.message("Figür başarıyla eklendi", {
        dismissOthers: true,
        messageType: MessageType.Success,
        position: Position.TopRight
      });
    }, errorMessage => {
      this.alertifyService.message(errorMessage, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopLeft
      });
    });
  }
}
