import { HttpInterceptorFn } from '@angular/common/http';

export const cacheBusterInterceptor: HttpInterceptorFn = (req, next) => {
  // Resim isteklerine cache buster ekle
  if (req.url.includes('amazon-images') || req.url.includes('wwwroot')) {
    const modifiedReq = req.clone({
      setParams: { t: Date.now().toString() }
    });
    return next(modifiedReq);
  }
  return next(req);
};