import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseDashboardComponent } from './warehouse-dashboard/warehouse-dashboard.component';

const routes: Routes = [
  { path: '', component: WarehouseDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseRoutingModule { }
