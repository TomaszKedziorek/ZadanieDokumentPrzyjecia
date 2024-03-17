import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { FormsModule } from '@angular/forms';

import { AdmissionDocumentRoutingModule } from './admission-document-routing.module';
import { AdmissionDocumentDashboardComponent } from './admission-document-dashboard/admission-document-dashboard.component';
import { AdmissionDocumentUpsertComponent } from './admission-document-upsert/admission-document-upsert.component';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

@NgModule({
  declarations: [
    AdmissionDocumentDashboardComponent,
    AdmissionDocumentUpsertComponent
  ],
  imports: [
    CommonModule,
    AdmissionDocumentRoutingModule,
    BsDropdownModule,
    CollapseModule,
    FormsModule,
    NgMultiSelectDropDownModule.forRoot()
  ]
})
export class AdmissionDocumentModule { }
