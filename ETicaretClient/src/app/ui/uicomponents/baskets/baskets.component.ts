import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { BaseComponent, spinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { BasketService } from '../../../services/common/models/basket.service';
import { NgFor, NgIf } from '@angular/common';
import { List_Basket_Item } from '../../../contracts/basket/list_basket_item';
import { Update_Quantity } from '../../../contracts/basket/update_quantity';
import { OrderService } from '../../../services/common/models/order.service';
import { Create_Order } from '../../../contracts/order/create_order';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';
import { Router } from '@angular/router';

declare var bootstrap: any;

@Component({
  selector: 'app-baskets',
  imports: [NgFor, NgIf],
  templateUrl: './baskets.component.html',
  styleUrl: './baskets.component.css'
})
export class BasketsComponent extends BaseComponent implements OnInit {

  basketItems: List_Basket_Item[] = [];
  removingItems = new Set<string>();
  updatingItems = new Set<string>();

  constructor(
    spinner: NgxSpinnerService,
    private basketService: BasketService,
    private cdr: ChangeDetectorRef,
    private orderService: OrderService,
    private toastrService: CustomToastrService,
    private router: Router
  ) {
    super(spinner);
  }
  dom() {
    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();
    }

    setTimeout(() => {
      const modalElement = document.getElementById('exampleModal');

      if (modalElement) {
        const modal = bootstrap.Modal.getInstance(modalElement);

        if (modal) {
          modal.dispose();
        }

        modalElement.classList.remove('show');
        modalElement.style.display = 'none';
        modalElement.setAttribute('aria-hidden', 'true');
        modalElement.removeAttribute('aria-modal');
        modalElement.removeAttribute('role');

        document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());

        document.body.classList.remove('modal-open');
        document.body.style.overflow = '';
        document.body.style.paddingRight = '';
      }
    }, 100);
  }

  async ngOnInit() {
    this.basketItems = await this.basketService.getBasket();
  }

  async increaseQuantity(basketItemId: string, index: number) {
    if (this.updatingItems.has(basketItemId)) return;

    this.updatingItems.add(basketItemId);
    const currentQuantity = this.basketItems[index].quantity;

    try {
      const basketItem = new Update_Quantity();
      basketItem.basketItemId = basketItemId;
      basketItem.quantity = currentQuantity + 1;

      await this.basketService.updateQuantity(basketItem);
      this.basketItems[index].quantity = currentQuantity + 1;
    } catch (error) {
      console.error('Güncelleme hatası:', error);
    } finally {
      this.updatingItems.delete(basketItemId);
      this.cdr.detectChanges();
    }
  }

  async decreaseQuantity(basketItemId: string, index: number) {
    if (this.updatingItems.has(basketItemId)) return;

    const currentQuantity = this.basketItems[index].quantity;
    if (currentQuantity <= 1) return;

    this.updatingItems.add(basketItemId);

    try {
      const basketItem = new Update_Quantity();
      basketItem.basketItemId = basketItemId;
      basketItem.quantity = currentQuantity - 1;

      await this.basketService.updateQuantity(basketItem);
      this.basketItems[index].quantity = currentQuantity - 1;
    } catch (error) {
      console.error('Güncelleme hatası:', error);
    } finally {
      this.updatingItems.delete(basketItemId);
      this.cdr.detectChanges();
    }
  }

  async removeItem(basketItemId: string, index: number) {
    if (this.removingItems.has(basketItemId)) return;

    this.removingItems.add(basketItemId);
    this.cdr.detectChanges();

    try {
      await this.basketService.removeBasketItem(basketItemId);
      this.basketItems.splice(index, 1);

      await new Promise(resolve => setTimeout(resolve, 250));

      if (this.basketItems.length === 0) {
        this.closeModal();
      }
    } catch (error) {
      console.error('Silme hatası:', error);
    } finally {
      this.removingItems.delete(basketItemId);
      this.cdr.detectChanges();
    }
  }

  closeModal() {
    this.dom()
  }

  async shoppingComplete() {
    this.showSpinnerWithoutTimeout(spinnerType.BallAtom)
    const order: Create_Order = new Create_Order()
    order.address = "Kocaeli",
      order.description = "tehlikeli yerler"
    await this.orderService.create(order)
    this.hideSpinner(spinnerType.BallAtom)
    this.toastrService.message("Başarili", "Siparis Olusturuldu", { messagetype: ToastrMessageType.Success, position: ToastrPosition.Topright })
    this.dom()
    this.router.navigate(["/home"])
  }
}