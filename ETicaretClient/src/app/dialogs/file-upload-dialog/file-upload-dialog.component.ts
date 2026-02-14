import { Component, ViewEncapsulation } from '@angular/core';
import { BaseDialogs } from '../base/base-dialogs';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-file-upload-dialog',
  imports: [MatDialogModule,
    MatButtonModule],
  templateUrl: './file-upload-dialog.component.html',
  styleUrl: './file-upload-dialog.component.css',
  encapsulation: ViewEncapsulation.None
})
export class FileUploadDialogComponent extends BaseDialogs<FileUploadDialogComponent> {
  constructor(dialogRef: MatDialogRef<FileUploadDialogComponent>) {
    super(dialogRef)
  }
}
