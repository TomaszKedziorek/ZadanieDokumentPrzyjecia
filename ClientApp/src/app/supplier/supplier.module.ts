import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SupplierRoutingModule } from './supplier-routing.module';
import { SupplierDashboardComponent } from './supplier-dashboard/supplier-dashboard.component';
import { SupplierUpsertComponent } from './supplier-upsert/supplier-upsert.component';

@NgModule({
  declarations: [
    SupplierDashboardComponent,
    SupplierUpsertComponent
  ],
  imports: [
    CommonModule,
    SupplierRoutingModule,
    FormsModule
  ]
})
export class SupplierModule { }
