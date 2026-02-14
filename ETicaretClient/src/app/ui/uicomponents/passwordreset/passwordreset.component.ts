import { Component } from '@angular/core';
import { BaseComponent, spinnerType } from '../../../base/base.component';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';
import { UserAuthService } from '../../../services/common/models/user-auth.service';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';

@Component({
  selector: 'app-passwordreset',
  imports: [],
  templateUrl: './passwordreset.component.html',
  styleUrl: './passwordreset.component.css'
})
export class PasswordresetComponent extends BaseComponent {
  constructor(spinner : NgxSpinnerService, private userauthservice : UserAuthService, private alertifyservice : AlertifyService){
    super(spinner)
  }
  passwordReset(email){
    this.showSpinnerWithoutTimeout(spinnerType.BallAtom)
    this.userauthservice.passwordReset(email,()=> {this.hideSpinner(spinnerType.BallAtom),
      this.alertifyservice.message("Mail Başarıyla Gönderilmiştir", {
        messageType : MessageType.Success,
        position : Position.TopRight
      })
    })
  }
}
