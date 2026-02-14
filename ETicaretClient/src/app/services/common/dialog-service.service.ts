import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentType } from '@angular/cdk/portal';

@Injectable({
  providedIn: 'root'
})
export class DialogServiceService {

  constructor(private dialog: MatDialog) { }

  openDialog(parameters: Partial<DialogParameters>): void {
    const dialogRef = this.dialog.open(parameters.componentType, {
      panelClass: 'custom-file-upload-dialog',
      width: '1000px',
      maxWidth: '100vw',
      data: parameters.data
    });
  }
}

export class DialogParameters {
  componentType: ComponentType<any>;
  data?: any
}