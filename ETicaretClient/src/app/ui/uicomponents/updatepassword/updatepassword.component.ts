import { Component, OnInit } from '@angular/core';
import { UserAuthService } from '../../../services/common/models/user-auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgIf } from '@angular/common';
import { BaseComponent, spinnerType } from '../../../base/base.component';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';
import { UserServiceService } from '../../../services/common/models/user-service.service';

@Component({
  selector: 'app-updatepassword',
  imports: [NgIf],
  templateUrl: './updatepassword.component.html',
  styleUrl: './updatepassword.component.css'
})
export class UpdatepasswordComponent extends BaseComponent implements OnInit {
  constructor(private userAuthService: UserAuthService, private router: Router,private activatedRoute: ActivatedRoute, spinner: NgxSpinnerService, private alertifyService: AlertifyService, private userService: UserServiceService) {
    super(spinner);
  }
  state: any
  ngOnInit() {
    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId = params["userId"]
        const resetToken = decodeURIComponent(params["resetToken"])

        this.state = await this.userAuthService.verifyResetToken(resetToken, userId)
        debugger;
      }
    })
  }
  updatePassword(password: string, passwordConfirm: string) {

    if (password != passwordConfirm) {
      this.alertifyService.message("Şifreleri doğru şekilde giriniz", {
        messageType: MessageType.Error,
        position: Position.TopRight
      })
      return;
    }
    try {

      this.activatedRoute.params.subscribe({
        next: async params => {
          const userId = params["userId"]
          const resetToken = (params["resetToken"])
          await this.userService.forgetPasswordUpdate(userId, resetToken, password, passwordConfirm)
          this.alertifyService.message("Şifre başarıyla güncellendi", {
            messageType: MessageType.Success,
            position: Position.TopRight
          });
          this.router.navigate(['/login']);
        }
      })
    }
    catch(error) {
      this.alertifyService.message("Şifre güncellenirken hata oluştu", {
      messageType: MessageType.Error,
      position: Position.TopRight
    });
    }
  }
}
