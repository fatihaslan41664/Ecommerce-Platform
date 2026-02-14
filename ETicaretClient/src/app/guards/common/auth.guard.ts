import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';
import { AuthService } from '../../services/common/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const toastrService = inject(CustomToastrService);
  const authService = inject(AuthService);

  authService.identityCheck();

  if (!authService.isauthenticated) {
    toastrService.message("Oturum açmanız gerekli", "Yetkisiz Erişim", {
      messagetype: ToastrMessageType.Warning,
      position: ToastrPosition.Topleft,
    });

    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  return true;
};

