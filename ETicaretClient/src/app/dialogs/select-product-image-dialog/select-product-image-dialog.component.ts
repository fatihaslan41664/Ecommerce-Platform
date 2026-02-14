import { Component, Inject, OnInit, ViewChild, ChangeDetectionStrategy } from '@angular/core';
import { BaseDialogs } from '../base/base-dialogs';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FileUploadComponent, UploadOptions } from '../../services/common/file-upload/file-upload.component';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/common/models/product.service';
import { List_product_Image } from '../../contracts/list_product_images';
import { alertifyOptions, AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';


@Component({
  selector: 'app-select-product-image-dialog',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule, FileUploadComponent, MatCardModule, CommonModule],
  templateUrl: './select-product-image-dialog.component.html',
  styleUrl: './select-product-image-dialog.component.css'
})
export class SelectProductImageDialogComponent extends BaseDialogs<SelectProductImageDialogComponent> implements OnInit {

  public options: Partial<UploadOptions> = {}; // @Output kaldırıldı
  @ViewChild(FileUploadComponent) fileUploadComponent!: FileUploadComponent;
  images: List_product_Image[] = [];
  constructor(
    dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    private productService: ProductService,
    private alertify: AlertifyService
    ,
    @Inject(MAT_DIALOG_DATA) public data: SelectImageState | string
  ) {
    super(dialogRef);
  }
  async ngOnInit() {
    this.options = {
      accept: ".png, .jpg, .jpeg, .gif",
      action: "Upload",
      controller: "Product",
      explain: "Urun resimlerini secin veya surukleyin",
      querystring: `id=${this.data}`,
      manualUpload: true
    };
    this.images = await this.productService.readImages(this.data as string);
  }

  onConfirm() {
    if (this.fileUploadComponent) {
      const fileCount = this.fileUploadComponent.getSelectedFileCount();
      if (fileCount > 0) {
        
        this.fileUploadComponent.triggerUpload();
        this.dialogRef.close(true);
      } else {
        
        this.dialogRef.close(false);
      }
    }
  }

  onCancel() {
    if (this.fileUploadComponent) {
      this.fileUploadComponent.clearFiles();
    }
    this.dialogRef.close(false);
  }
  async deleteImage(id: string) {
    try {
      await this.productService.deleteİmage(this.data as string, id);

      this.alertify.message('Resim başarıyla silindi', {
        messageType: MessageType.Success,
        position: Position.BottomRight,
        delay: 3,
        dismissOthers: true

      });

      this.images = this.images.filter(img => img.id !== id);
    } catch (error) {
      console.error('Silme hatası:', error);
    }
  }
  showCase(imageId: string) {
    this.productService.changeShowImage(imageId, this.data as string)
    this.alertify.message("vitrin resmi değiştirildi", {
      messageType: MessageType.Success,
      position: Position.BottomRight,
      delay: 2,
      dismissOthers: true
    })
  }
}

export enum SelectImageState {
  Close
}