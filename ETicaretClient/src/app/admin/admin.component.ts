import { Component } from '@angular/core';
import { LayoutComponent } from './layout/layout.component';
import { ComponentsComponent } from './layout/components/components.component';
import { RouterOutlet } from '@angular/router';



@Component({
  selector: 'app-admin',
  imports: [RouterOutlet],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

}
