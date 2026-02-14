import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {
  private baseUrl: string = "https://localhost:44373/api";
  constructor(private httpclient: HttpClient) {
  }


  private getToken(): string | null {
    return localStorage.getItem("accessToken");
  }

  private getAuthorizationHeaders(requestParameters: Partial<RequestParameters>): HttpHeaders {
    let headers = requestParameters.headers || new HttpHeaders();

    if (!headers.has('Authorization')) {
      const token = this.getToken();
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }

    return headers;
  }

  private url(requestParameters: Partial<RequestParameters>): string {
    return `${requestParameters.baseUrl ? requestParameters.baseUrl : this.baseUrl}/${requestParameters.controller}${requestParameters.action ? `/${requestParameters.action}` : ""}`;
  }

  get<T>(requestParameters: Partial<RequestParameters>, id?: string): Observable<T> {
    let mainurl: string;

    if (requestParameters.fullEndPoint) {
      mainurl = requestParameters.fullEndPoint;
    } else {
      mainurl = `${this.url(requestParameters)}${id ? `/${id}` : ""}`;
      if (requestParameters.queryString) {
        mainurl += `?${requestParameters.queryString}`;
      }
    }
    const headers = this.getAuthorizationHeaders(requestParameters);


    return this.httpclient.get<T>(mainurl, {
      headers: headers
    });
  }

  put<T>(requestParameters: Partial<RequestParameters>, body?: Partial<T>): Observable<T> {
    let mainurl: string = "";

    if (requestParameters.fullEndPoint) {
      mainurl = requestParameters.fullEndPoint
    } else {
      mainurl = `${this.url(requestParameters)}`
    }

    if (requestParameters.queryString) {
      mainurl += `?${requestParameters.queryString}`;
    }

    const headers = this.getAuthorizationHeaders(requestParameters);

    // ✅ DÜZELTME: Body null olsa bile boş obje gönder, headers options'da olmalı
    return this.httpclient.put<T>(mainurl, body || {}, { headers: headers });
  }

  post<T>(requestParameters: Partial<RequestParameters>, body: Partial<T>): Observable<T> {
    let mainurl: string = "";

    if (requestParameters.fullEndPoint) {
      mainurl = requestParameters.fullEndPoint
    } else {
      mainurl = `${this.url(requestParameters)}`;
    }

    if (requestParameters.queryString) {
      mainurl += `?${requestParameters.queryString}`;
    }

    // Token'i içeren başlıkları al
    const headers = this.getAuthorizationHeaders(requestParameters);

    return this.httpclient.post<T>(mainurl, body, { headers: headers })
  }

  delete<T>(requestParameters: Partial<RequestParameters>, id: string): Observable<T> {
    let mainurl: string = "";

    if (requestParameters.fullEndPoint) {
      mainurl = requestParameters.fullEndPoint;
    } else {
      mainurl = `${this.url(requestParameters)}/${id}`;
      if (requestParameters.queryString) {
        mainurl += `?${requestParameters.queryString}`;
      }
    }

    // Token'i içeren başlıkları al (Önceki hatayı düzelten kısım)
    const headers = this.getAuthorizationHeaders(requestParameters);

    return this.httpclient.delete<T>(mainurl, {
      headers: headers // Token'li başlıklar kullanılıyor
    });
  }
}

// RequestParameters sınıfında bir değişiklik yapılmadı, olduğu gibi kalabilir.
export class RequestParameters {
  controller?: string;
  action?: string;
  headers?: HttpHeaders;
  baseUrl?: string;
  fullEndPoint?: string;
  queryString?: string;
}