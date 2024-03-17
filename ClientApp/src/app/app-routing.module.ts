import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './core/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: 'home', component: HomeComponent, pathMatch: "full" },
  { path: 'warehouses', loadChildren: () => import('./warehouse/warehouse.module').then(mod => mod.WarehouseModule) },
  { path: 'commodities', loadChildren: () => import('./commodity/commodity.module').then(mod => mod.CommodityModule) },
  { path: 'suppliers', loadChildren: () => import('./supplier/supplier.module').then(mod => mod.SupplierModule) },
  { path: 'admission-documents', loadChildren: () => import('./admission-document/admission-document.module').then(mod => mod.AdmissionDocumentModule) },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
