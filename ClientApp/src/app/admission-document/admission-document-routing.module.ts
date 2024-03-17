import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdmissionDocumentDashboardComponent } from './admission-document-dashboard/admission-document-dashboard.component';
import { AdmissionDocumentUpsertComponent } from './admission-document-upsert/admission-document-upsert.component';

const routes: Routes = [
  { path: "", component: AdmissionDocumentDashboardComponent },
  { path: "new", component: AdmissionDocumentUpsertComponent },
  { path: "edit/:id", component: AdmissionDocumentUpsertComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdmissionDocumentRoutingModule { }
