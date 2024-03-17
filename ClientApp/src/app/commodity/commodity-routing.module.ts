import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommodityDashboardComponent } from './commodity-dashboard/commodity-dashboard.component';
import { CommodityUpsertComponent } from './commodity-upsert/commodity-upsert.component';


const routes: Routes = [
  { path: '', component: CommodityDashboardComponent },
  { path: 'new', component: CommodityUpsertComponent },
  { path: 'edit/:id', component: CommodityUpsertComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CommodityRoutingModule { }
