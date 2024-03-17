import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CommodityRoutingModule } from './commodity-routing.module';
import { CommodityDashboardComponent } from './commodity-dashboard/commodity-dashboard.component';
import { CommodityUpsertComponent } from './commodity-upsert/commodity-upsert.component';

@NgModule({
  declarations: [
    CommodityDashboardComponent,
    CommodityUpsertComponent
  ],
  imports: [
    CommonModule,
    CommodityRoutingModule,
    FormsModule
  ]
})
export class CommodityModule { }
