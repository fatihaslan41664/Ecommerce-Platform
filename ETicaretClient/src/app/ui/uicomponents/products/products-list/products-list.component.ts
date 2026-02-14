import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../../services/common/models/product.service';
import { List_product } from '../../../../contracts/list_product';
import { NgFor, CurrencyPipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BaseComponent, spinnerType } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { BasketService } from '../../../../services/common/models/basket.service';
import { Create_Basket_Item } from '../../../../contracts/basket/create_basket_item';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-products-list',
  imports: [NgFor, CurrencyPipe, RouterLink],
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.css'
})
export class ProductsListComponent extends BaseComponent implements OnInit {

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute, spinner: NgxSpinnerService, private basketService: BasketService, private toastrService: CustomToastrService) {
    super(spinner)
  }
  TotalProductCount: number;
  TotalPageCount;
  currentPageNo: number;
  produtcs: List_product[];
  pageSize: number = 10;
  pageList: number[] = [];
  async ngOnInit() {
    this.activatedRoute.params.subscribe(async params => {
      this.currentPageNo = parseInt(params["pageNo"] ?? 1)
      const data = await this.productService.read(this.currentPageNo - 1, this.pageSize, () => { })

      this.produtcs = data.products.map(product => {
        // ✅ showCase: true olan resmi bul (KÜÇÜK HARF)
        const showcaseImage = product.productImages?.find(img => img.showCase === true);
        return {
          ...product,
          imagePath: showcaseImage?.path || null // ✅ Path'i al
        };
      });
      this.TotalProductCount = data.totalcount
      this.TotalPageCount = Math.round(this.TotalProductCount / this.pageSize);
      this.pageList = [];

      if (this.TotalPageCount <= 7) {
        // Toplam sayfa 7 veya daha az ise hepsini göster
        for (let i = 1; i <= this.TotalPageCount; i++) {
          this.pageList.push(i);
        }
      }
      else if (this.currentPageNo <= 4) {
        // Başlangıçta: 1-7 arası göster
        for (let i = 1; i <= 7; i++) {
          this.pageList.push(i);
        }
      }
      else if (this.currentPageNo >= this.TotalPageCount - 3) {
        // Son sayfalarda: son 7 sayfayı göster
        for (let i = this.TotalPageCount - 6; i <= this.TotalPageCount; i++) {
          this.pageList.push(i);
        }
      }
      else {
        // Ortada: mevcut sayfa etrafında 7 sayfa göster
        for (let i = this.currentPageNo - 3; i <= this.currentPageNo + 3; i++) {
          this.pageList.push(i);
        }
      }
    })
  }
  async addToBasket(product: List_product) {
    this.showSpinner(spinnerType.BallAtom)
    let _basketItem: Create_Basket_Item = new Create_Basket_Item()
    _basketItem.productId = product.id,
      _basketItem.quantity = 1
    await this.basketService.addBasket(_basketItem)
    this.hideSpinner(spinnerType.BallAtom);
    this.toastrService.message("Ürün sepete eklenmiştir", "Başarılı", {
      messagetype: ToastrMessageType.Success,
      position: ToastrPosition.Topleft
    })
  }
}
