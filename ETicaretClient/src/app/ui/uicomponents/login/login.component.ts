import { AfterViewInit, Component, OnInit } from '@angular/core';
import { UserServiceService } from '../../../services/common/models/user-service.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';
import { BaseComponent, spinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from '../../../services/common/auth.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { SocialAuthService, SocialUser, GoogleSigninButtonModule } from '@abacritt/angularx-social-login';
import { UserAuthService } from '../../../services/common/models/user-auth.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [GoogleSigninButtonModule,RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent extends BaseComponent implements OnInit, AfterViewInit {
  private returnUrl: string | null = null;

  constructor(
    private userService: UserServiceService,
    private activatedRoute: ActivatedRoute,
    spinner: NgxSpinnerService,
    private authService: AuthService,
    private router: Router,
    private socialAuthService: SocialAuthService,
    private userAuthService: UserAuthService,
    private toastr: CustomToastrService
  ) {
    super(spinner);

    // returnUrl'i yakala
    const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];
    if (returnUrl) {
      this.returnUrl = returnUrl;
      localStorage.setItem('pendingReturnUrl', returnUrl);
      console.log('💾 Constructor - saved returnUrl:', returnUrl);
    }
  }

  ngOnInit(): void {
    console.log('📍 ngOnInit - returnUrl:', this.returnUrl);
  }

  ngAfterViewInit(): void {
    // authState burada dinlenmeli (parametreler oturduktan sonra)
    this.socialAuthService.authState
      .pipe(first())
      .subscribe(async (user: SocialUser) => {
        if (!user) return;
        this.showSpinner(spinnerType.BallAtom);
        const savedReturnUrl = localStorage.getItem('pendingReturnUrl');
        await this.userAuthService.googleLogin(user, async () => {
          this.authService.identityCheck();

          // 🔹 Yönlendirme önceliği: localStorage > query > default
          const target = savedReturnUrl || this.returnUrl || '/products';
          await this.router.navigate([target]);

          localStorage.removeItem('pendingReturnUrl');
          this.returnUrl = null;

          this.hideSpinner(spinnerType.BallAtom);
          this.toastr.message('Google ile giriş yapıldı', 'Giriş Başarılı', {
            messagetype: ToastrMessageType.Success,
            position: ToastrPosition.Topleft
          });
        });
      });
  }
  async login(email: string, password: string): Promise<void> {
    this.showSpinner(spinnerType.BallAtom);

    await this.userAuthService.login(email, password, async () => {
      this.authService.identityCheck();

      const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];
      const target = returnUrl ? returnUrl : '/products';
      this.router.navigate([target]);

      this.hideSpinner(spinnerType.BallAtom);
    });
  }
}
