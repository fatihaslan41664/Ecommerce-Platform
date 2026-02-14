// FileUploadComponent'e bu methodları ekleyin:

import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NgxFileDropEntry, FileSystemFileEntry, NgxFileDropModule } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { MatDialog } from '@angular/material/dialog';
import { FileUploadDialogComponent } from '../../../dialogs/file-upload-dialog/file-upload-dialog.component';

@Component({
  selector: 'app-file-upload',
  imports: [NgxFileDropModule, CommonModule],
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css'],
})
export class FileUploadComponent {
  constructor(
    private httpclient: HttpClientService,
    private alertify: AlertifyService,
    private tostr: CustomToastrService,
    private dialog: MatDialog
  ) { }

  public files: NgxFileDropEntry[] = [];
  @Input() options: Partial<UploadOptions>;

  public SelectedFiles(files: NgxFileDropEntry[]) {
    this.files = files; // Dosyaları sadece sakla

    // Eğer manualUpload true ise sadece dosyaları sakla, upload yapma
    if (this.options?.manualUpload === true) {
      console.log('Manuel upload modu, dosyalar seçildi:', files.length);
      return;
    }

    // Eğer showConfirmDialog false ise direkt upload yap
    if (this.options?.showConfirmDialog === false) {
      console.log('Onay dialog\'u atlanıyor, direkt upload başlıyor...');
      this.uploadFiles(files);
      return;
    }

    // Varsayılan olarak dialog aç (geriye uyumluluk için)
    const dialogRef = this.dialog.open(FileUploadDialogComponent, {
      width: '400px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        console.log('Dialog onaylandı, upload başlıyor...');
        this.uploadFiles(files);
      } else {
        console.log('Dialog iptal edildi');
      }
    });
  }

  // Dışarıdan çağrılabilir upload methodu
  public triggerUpload() {
    if (this.files && this.files.length > 0) {
      this.uploadFiles(this.files);
    } else {
      console.log('Upload için seçilmiş dosya bulunamadı');
    }
  }

  // Seçilmiş dosyaları temizle
  public clearFiles() {
    this.files = [];
  }

  // Seçilmiş dosya sayısını döndür
  public getSelectedFileCount(): number {
    return this.files ? this.files.length : 0;
  }

  private async uploadFiles(files: NgxFileDropEntry[]) {
    console.log('uploadFiles çağrıldı, dosya sayısı:', files.length);
    console.log('Options:', this.options);

    const fileData: FormData = new FormData();

    const filePromises = files.map(file => {
      return new Promise<void>((resolve, reject) => {
        const fileEntry = file.fileEntry as FileSystemFileEntry;
        fileEntry.file((_file: File) => {
          console.log('Dosya FormData\'ya eklendi:', _file.name, _file.size);
          fileData.append(_file.name, _file, file.relativePath);
          resolve();
        },);
      });
    });

    try {
      await Promise.all(filePromises);

      console.log('FormData hazır, HTTP isteği gönderiliyor...');

      this.httpclient.post(
        {
          controller: this.options.controller,
          action: this.options.action,
          queryString: this.options.querystring,
        },
        fileData
      ).subscribe({
        next: (data) => {
          console.log('Upload başarılı:', data);
          const Truemessage = "Dosyalar Başarıyla Yüklenmiştir";

          if (this.options.isAdminPage) {
            this.alertify.message(Truemessage, {
              dismissOthers: true,
              messageType: MessageType.Success,
              position: Position.TopRight
            });
          } else {
            this.tostr.message(Truemessage, "Başarılı", {
              messagetype: ToastrMessageType.Success,
              position: ToastrPosition.Topleft
            });
          }
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.error('Upload hatası:', errorResponse);
          const FalseMessage = "Dosyalar Yüklenemedi";
          if (this.options.isAdminPage) {
            this.alertify.message(FalseMessage, {
              dismissOthers: true,
              messageType: MessageType.Error,
              position: Position.TopRight
            });
          } else {
            this.alertify.message(FalseMessage, {
              dismissOthers: true,
              messageType: MessageType.Error,
              position: Position.TopRight
            });
          }
        }
      });

    } catch (error) {
      console.error('Promise.all hatası:', error);
      this.alertify.message('Dosya okuma hatası', {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
    }
  }
}

export class UploadOptions {
  controller?: string;
  action?: string;
  querystring?: string;
  explain?: string;
  accept?: string;
  isAdminPage: boolean = false;
  showConfirmDialog?: boolean = true;
  manualUpload?: boolean = false; // Yeni option
}