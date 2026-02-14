import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private baseSignalRUrl = "https://localhost:44373/";
  private _connection!: HubConnection;

  get connection(): HubConnection {
    return this._connection;
  }

  async start(hubUrl: string): Promise<void> {
    hubUrl = this.baseSignalRUrl + hubUrl;

    if (this._connection?.state === HubConnectionState.Connected) {
      console.log("✔ Zaten bağlı");
      return;
    }

    this._connection = new HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect()
      .build();

    // Event'leri bağlantıdan ÖNCE ekle
    this._connection.onreconnected(() => console.log("🔄 Yeniden bağlandı"));
    this._connection.onreconnecting(() => console.log("⏳ Yeniden bağlanılıyor..."));
    this._connection.onclose(() => console.log("❌ Bağlantı kapandı"));

    try {
      await this._connection.start();
      console.log("✔ SignalR bağlantı kuruldu:", hubUrl);
    } catch (error) {
      console.error("❌ Bağlantı hatası:", error);
      setTimeout(() => this.start(hubUrl), 2000);
    }
  }

  on(methodName: string, callBack: (...args: any[]) => void): void {
    if (!this._connection) {
      console.warn(`⚠️ ${methodName} için bağlantı henüz hazır değil`);
      return;
    }
    this._connection.on(methodName, callBack);
  }

  invoke(methodName: string, message: any, success?: any, error?: any): void {
    if (!this._connection || this._connection.state !== HubConnectionState.Connected) {
      console.error("❌ SignalR bağlantısı yok!");
      return;
    }

    this._connection.invoke(methodName, message)
      .then(success)
      .catch(error);
  }
}