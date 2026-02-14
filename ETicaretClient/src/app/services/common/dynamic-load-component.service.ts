import { ViewContainerRef, Injectable, Type, createComponent, EnvironmentInjector } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DynamicLoadComponentService {

  constructor(private environmentInjector: EnvironmentInjector) {
  }
  
  async loadComponent(componentName: ComponentName, viewContainerRef: ViewContainerRef) {
    let _component: any = null;
    
    switch(componentName) {
      case ComponentName.BasketsComponent:
        const module = await import("../../ui/uicomponents/baskets/baskets.component");
        _component = module.BasketsComponent;
        break;
    }
    
    viewContainerRef.clear();
    return viewContainerRef.createComponent(_component, {
      environmentInjector: this.environmentInjector
    });
  }
  
}

export enum ComponentName {
  BasketsComponent
}