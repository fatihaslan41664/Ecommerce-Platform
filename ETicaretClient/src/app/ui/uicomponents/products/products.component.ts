import { Component } from '@angular/core';
import { HttpClientService } from '../../../services/common/http-client.service';
import { data } from 'jquery';
import { ProductsListComponent } from './products-list/products-list.component';

@Component({
  selector: 'app-products',
  imports: [ProductsListComponent],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {

}

