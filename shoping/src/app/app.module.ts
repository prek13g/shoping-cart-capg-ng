import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { RouterModule,Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CategoryComponent } from './category/category.component';
import { ProductComponent } from './product/product.component';
import { UserComponent } from './user/user.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CategoryService } from './category.service';
import { FormsModule } from '@angular/forms';
import { ProductService } from './product.service';
import { UserService } from './user.service';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { FooterComponent } from './footer/footer.component';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { UserloginService } from './userlogin.service';
import { CartComponent } from './cart/cart.component';
import { CartService } from './cart.service';
import { QuantityService } from './quantity.service';
import { PyamentService } from './pyament.service';
import { PaymentComponent } from './payment/payment.component';
import { OrderComponent } from './order/order.component';
import { OrderService } from './order.service';
import { OutputComponent } from './output/output.component';
import { AuthInterceptor } from './auth.interceptor';
import { AdminComponent } from './admin/admin.component';
import { AdminService } from './admin.service';
import { AdminlogoutComponent } from './adminlogout/adminlogout.component';










const routes:Routes=[
  {
    path:"",component:HomeComponent
  },
  {
    path:"Category",component:CategoryComponent
  },
  {
    path:"Product",component:ProductComponent
  },
  {
    path:"User",component:UserComponent
  },
  {
    path:"product_detail/:id",component:ProductDetailComponent
  },
  {
   path:"signup",component:SignupComponent
  },
  {
    path:"signin",component:SigninComponent
  },
  {
    path:"cart/:id",component:CartComponent
  },
  {
    path:"cart",component:CartComponent
  },
  {
    path:"payment",component:PaymentComponent
  },
  
  {
    path:"payment/:id",component:PaymentComponent
  },
  {
    path:"order",component:OrderComponent
  },
  {
    path:"logout",component:OutputComponent
  },
  {
    path:"admin",component:AdminComponent
  },
  {
    path:"admin/logout",component:AdminlogoutComponent
  },
  {
    path:"header",component:HeaderComponent
  },{
    path:"appcomponent",component:AppComponent
  }

  

  
  
 
]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    CategoryComponent,
    ProductComponent,
    UserComponent,
    ProductDetailComponent,
    FooterComponent,
    SignupComponent,
    SigninComponent,
    CartComponent,
    PaymentComponent,
    OrderComponent,
    OutputComponent,
    AdminComponent,
    AdminlogoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    CategoryService,
    ProductService,
    UserService,
    UserloginService,
    CartService,
    QuantityService,
    PyamentService,
    OrderService,
    AdminService,
    provideClientHydration(),
    {provide:HTTP_INTERCEPTORS,useClass: AuthInterceptor,multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
