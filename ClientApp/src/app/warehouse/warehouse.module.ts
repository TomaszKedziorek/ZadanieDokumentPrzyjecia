import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarehouseDashboardComponent } from './warehouse-dashboard/warehouse-dashboard.component';
import { WarehouseRoutingModule } from './warehouse-routing.module';


@NgModule({
  declarations: [
    WarehouseDashboardComponent
  ],
  imports: [
    CommonModule,
    WarehouseRoutingModule
  ],
  exports: [
    WarehouseDashboardComponent
  ]
})
export class WarehouseModule { }
