import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SupplierDashboardComponent } from './supplier-dashboard/supplier-dashboard.component';
import { SupplierUpsertComponent } from './supplier-upsert/supplier-upsert.component';

const routes: Routes = [
  { path: '', component: SupplierDashboardComponent },
  { path: 'new', component: SupplierUpsertComponent },
  { path: 'edit/:id', component: SupplierUpsertComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SupplierRoutingModule { }
