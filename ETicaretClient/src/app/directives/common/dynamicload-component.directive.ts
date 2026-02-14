import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appDynamicloadComponent]'
})
export class DynamicloadComponentDirective {

  constructor(public viewContainerRef: ViewContainerRef) { }

}
