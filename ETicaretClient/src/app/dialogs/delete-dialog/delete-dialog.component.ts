import { Component, Inject, inject, model, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { BaseDialogs } from '../base/base-dialogs';

@Component({
  selector: 'app-delete-dialog',
  imports: [MatDialogModule],
  templateUrl: './delete-dialog.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./delete-dialog.component.css']

})
export class DeleteDialogComponent extends BaseDialogs<DeleteDialogComponent> {
  constructor(
    dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DeleteState | string
  ) {
    super(dialogRef);
  }
}

export enum DeleteState {
  Yes,
  No
}