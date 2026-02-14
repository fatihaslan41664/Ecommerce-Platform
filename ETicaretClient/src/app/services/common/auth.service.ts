import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper: JwtHelperService) {
  }
  identityCheck(){
    const token: string | null = localStorage.getItem("accessToken");
    let expired: boolean;

    try {
      expired = token ? this.jwtHelper.isTokenExpired(token) : true;
    } catch {
      expired = true;
    }
    _isAuthenticated = token != null && !expired
    
  }
  get isauthenticated():boolean{
    return _isAuthenticated;
  }
}
export let _isAuthenticated : boolean;