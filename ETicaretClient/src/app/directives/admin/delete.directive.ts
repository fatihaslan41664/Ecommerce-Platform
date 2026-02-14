import { Directive, ElementRef, HostListener, inject, Input, model, Renderer2, signal } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent, DeleteState } from '../../dialogs/delete-dialog/delete-dialog.component';
import { HttpClientService } from '../../services/common/http-client.service';
import { firstValueFrom } from 'rxjs';
import { AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';
@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {
  readonly dialog = inject(MatDialog);
  readonly animal = signal('');
  readonly name = model('');
  constructor(private element: ElementRef,
    private _render: Renderer2,
    private httpcleintservice: HttpClientService,
    private alertifyService: AlertifyService
  ) {
    const img = _render.createElement("img");
    img.setAttribute("src", "../../../../../assets/Deleteicon.png");
    img.setAttribute("style", "cursor:pointer;");
    img.width = 25;
    img.height = 25;
    _render.appendChild(element.nativeElement, img);
  }
  @Input() id: string;
  @Input() controller: string;
  @HostListener("click")
  onClick() {
    this.openDialog(() => {
      const td: HTMLElement = this.element.nativeElement;
      const tr = td.parentElement;

      if (tr) {
        this._render.addClass(tr, 'fade-out');

        this.httpcleintservice.delete({
          controller: this.controller
        }, this.id).subscribe({
          next: () => {
            setTimeout(() => {
              this._render.removeChild(tr.parentElement, tr);
            }, 300);
            this.alertifyService.message("Silme işlemi başarılı", {
              messageType: MessageType.Success,
              position: Position.TopLeft
            });
          },
          error: err => {
            this._render.removeClass(tr, 'fade-out');
            this.alertifyService.message("Silme işlemi başarısız", {
              messageType: MessageType.Error,
              position: Position.TopCenter
            });
            console.error("Silme hatası:", err);
          }
        });
      }
    });
  }
  openDialog(afterClosed: () => void): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        afterClosed();
      }
    });
  }

}
