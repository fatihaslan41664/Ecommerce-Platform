import { Routes } from '@angular/router';
import { BasketsComponent } from './ui/uicomponents/baskets/baskets.component';
import { HomeComponent } from './ui/uicomponents/home/home.component';
import { ProductsComponent } from './ui/uicomponents/products/products.component';
import { CustomerComponent } from './admin/layout/components/customer/customer.component';
import { OrderComponent } from './admin/layout/components/order/order.component';
import { ProductComponent } from './admin/layout/components/product/product.component';
import { DashboardComponent } from './admin/layout/components/dashboard/dashboard.component';
import { LayoutComponent } from './admin/layout/layout.component';
import { UiComponent } from './ui/ui.component';
import { RegisterComponent } from './ui/uicomponents/register/register.component';
import { LoginComponent } from './ui/uicomponents/login/login.component';
import { authGuard } from './guards/common/auth.guard';
import { PasswordresetComponent } from './ui/uicomponents/passwordreset/passwordreset.component';
import { UpdatepasswordComponent } from './ui/uicomponents/updatepassword/updatepassword.component';
import { AuthorizeMenuComponent } from './admin/layout/components/authorize-menu/authorize-menu.component';
import { RoleComponent } from './admin/layout/components/role/role.component';
import { UserComponent } from './admin/layout/components/user/user.component';

export const routes: Routes = [

    {
        path: '',
        redirectTo: 'uicomponent/home',
        pathMatch: 'full',
    },
    {
        path: '',
        component: UiComponent,
        children: [
            { path: 'home', component: HomeComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'products/:pageNo', component: ProductsComponent },
            { path: 'register', component: RegisterComponent },
            { path: 'login', component: LoginComponent },
            { path: 'sifremiunuttum', component: PasswordresetComponent },
            { path: 'sifreyiguncelle/:userId/:resetToken', component: UpdatepasswordComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
        ],
    },
    {
        path: 'admin',
        component: LayoutComponent,
        children: [
            { path: '', component: DashboardComponent },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'customer', component: CustomerComponent },
            { path: 'order', component: OrderComponent },
            { path: 'product', component: ProductComponent },
            { path: 'authorize-menu', component: AuthorizeMenuComponent},
            { path : 'roles', component:RoleComponent},
            { path : 'users', component:UserComponent}
        ], canActivate: [authGuard],
    },
];
