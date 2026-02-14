import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { User } from '../../../entities/user';
import { UserServiceService } from '../../../services/common/models/user-service.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';
import { RouterLink } from '@angular/router';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, CommonModule,RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent extends BaseComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private userService: UserServiceService, private toastrService: CustomToastrService, spinner : NgxSpinnerService) {
    super(spinner)
  }
  passwordsMatching = false;
  frm!: FormGroup // ! ekledim, çünkü ngOnInit'te atanıyor
  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      nameSurname: ["", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50)
      ]],
      userName: ["", [
        Validators.required,
        Validators.maxLength(50),
        Validators.minLength(2)
      ]],
      email: ["", [
        Validators.required,
        Validators.email
      ]],
      password: ["", [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(16)
      ]],
      coniformPassword: ["", [
        Validators.required,
      ]]
    },
      {
        validators: (group: AbstractControl): ValidationErrors | null => {
          // Kontrolün null olup olmadığını kontrol ederek olası hatayı önlüyoruz
          let passwordControl = group.get("password");
          let coniformPasswordControl = group.get("coniformPassword");

          if (!passwordControl || !coniformPasswordControl) {
            return null;
          }

          return passwordControl.value === coniformPasswordControl.value ? null : { notSame: true };
        }
      }
    )
  }
  get components() {
    // frm'in atanıp atanmadığını kontrol ediyoruz
    return this.frm ? this.frm.controls : {};
  }
  passwordVisible: boolean = false;
  togglePassword(): void {
    this.passwordVisible = !this.passwordVisible;
  }

  async onSubmit(user: User) {
    // Formdaki tüm alanları dokunulmuş olarak işaretle
    this.frm.markAllAsTouched();

    // Eğer form geçersiz ise (validasyon hatası varsa) işlemi durdur.
    if (this.frm.invalid) {
      this.toastrService.message("Lütfen tüm alanları doğru ve eksiksiz doldurunuz.", "Form Geçersiz", {
        messagetype: ToastrMessageType.Warning,
        position: ToastrPosition.Topright
      });
      return; // Backend'e istek gönderme
    }

    // Form geçerliyse backend isteği gönderilir
    const result = await this.userService.create(user)

    // Backend yanıtının null/undefined kontrolü (Önceki hatayı çözmek için eklenmişti, yerinde kalıyor)
    if (result) {
      // .succeeded kontrolü
      if (result.succeeded) {
        this.toastrService.message(result.message, "Kullanıcı Kaydı Başarılı", {
          messagetype: ToastrMessageType.Success,
          position: ToastrPosition.Topright
        })
      }
      else {
        this.toastrService.message(result.message || "Bilinmeyen bir hata oluştu.", "Kullanıcı Kaydı Başarısız", {
          messagetype: ToastrMessageType.Error,
          position: ToastrPosition.Topright
        })
      }
    } else {
      this.toastrService.message("Beklenmeyen bir hata oluştu veya sunucudan yanıt alınamadı. Lütfen daha sonra tekrar deneyin.", "Kayıt Başarısız", {
        messagetype: ToastrMessageType.Error,
        position: ToastrPosition.Topright
      })
    }
  }
}