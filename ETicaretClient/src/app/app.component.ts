import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NgxSpinnerModule, NgxSpinnerService } from "ngx-spinner";
import { HttpClientService } from './services/common/http-client.service';
import { BasketsComponent } from './ui/uicomponents/baskets/baskets.component';
import { BasketService } from './services/common/models/basket.service';
declare var $: any

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  constructor(private spinner: NgxSpinnerService, private httpclientservice: BasketService) {

  }

  ngOnInit() {

    setTimeout(() => {
      this.spinner.hide();
    }, 500);
    ;
  }
}

