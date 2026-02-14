import { Component, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../services/ui/custom-toastr.service';
import { error } from 'console';
import { AuthService } from '../services/common/auth.service';
import { CommonModule } from '@angular/common';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { ComponentName, DynamicLoadComponentService } from '../services/common/dynamic-load-component.service';
import { DynamicloadComponentDirective } from '../directives/common/dynamicload-component.directive';
declare var bootstrap: any;


@Component({
  selector: 'app-ui',
  imports: [RouterOutlet, RouterLink, CommonModule,DynamicloadComponentDirective],
  templateUrl: './ui.component.html',
  styleUrl: './ui.component.css'
})
export class UiComponent {
  @ViewChild(DynamicloadComponentDirective,{static : true})
  dynamicloadComponentDirective : DynamicloadComponentDirective;
  

  constructor(private toastrservice: CustomToastrService, public authservice: AuthService, public router: Router, public socialAuthService: SocialAuthService,
    private dynamicLoadComponentService : DynamicLoadComponentService
  ) {

    authservice.identityCheck();
  }
  signOut() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    this.authservice.identityCheck();
    this.router.navigate(["home"]);
    this.socialAuthService.signOut(true);
    this.toastrservice.message("oturum kapatılmıştır", "oturum kapandı", {
      messagetype: ToastrMessageType.Info,
      position: ToastrPosition.Topright,
    })
  }
    async loadComponent() {
    if (this.dynamicloadComponentDirective) {
      // Önce component'i yükle
      await this.dynamicLoadComponentService.loadComponent(
        ComponentName.BasketsComponent,
        this.dynamicloadComponentDirective.viewContainerRef
      );
      
      // Sonra modal'ı aç
      const modalElement = document.getElementById('exampleModal');
      if (modalElement) {
        const modal = new bootstrap.Modal(modalElement);
        modal.show();
      }
    }
  }
}
