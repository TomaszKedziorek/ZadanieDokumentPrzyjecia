import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { RouterModule } from "@angular/router";
import { FooterComponent } from './footer/footer.component';
import { HomeComponent } from './home/home.component';
import { WarehouseModule } from '../warehouse/warehouse.module';


@NgModule({
  declarations: [
    NavBarComponent,
    FooterComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CollapseModule,
    RouterModule,
    WarehouseModule
  ],
  exports: [
    NavBarComponent,
    FooterComponent
  ]
})
export class CoreModule { }
