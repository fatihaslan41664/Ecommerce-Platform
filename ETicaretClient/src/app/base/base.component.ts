import { NgxSpinnerService } from "ngx-spinner";

export class BaseComponent {
  constructor(private spinner: NgxSpinnerService) {
  }
  showSpinner(spinnerNameType: spinnerType) {
    this.spinner.show(spinnerNameType);

    setTimeout(() => this.hideSpinner(spinnerNameType), 500)
  }
  hideSpinner(spinnerNameType: spinnerType) {
    this.spinner.hide(spinnerNameType)
  }
    showSpinnerWithoutTimeout(spinnerNameType: spinnerType) {
    this.spinner.show(spinnerNameType);
  }
}
export enum spinnerType {
  BallAtom = "s1",
  BallScaleMultiple = "s2",
  BallSpinClockFadeRotating = "s3"
}
