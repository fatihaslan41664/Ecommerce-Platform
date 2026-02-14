import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, spinnerType } from '../../../../base/base.component';
import { OrderlistComponent } from './orderlist/orderlist.component';

@Component({
  selector: 'app-order',
  imports: [OrderlistComponent],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent extends BaseComponent {
  constructor(spinner: NgxSpinnerService) {
    super(spinner);
    this.showSpinner(spinnerType.BallSpinClockFadeRotating)
  }
}
