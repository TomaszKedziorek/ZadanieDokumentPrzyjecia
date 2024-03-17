import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { IAdmissionDocument } from 'src/app/shared/models/IAdmissionDocument';
import { AdmissionDocumentService } from '../admission-document.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admission-document-dashboard',
  templateUrl: './admission-document-dashboard.component.html'
})
export class AdmissionDocumentDashboardComponent implements OnInit {

  public documents!: IAdmissionDocument[];

  constructor(
    private service: AdmissionDocumentService,
    private router: Router,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
    this.getDocuments();
  }


  private getDocuments(): void {
    this.service.getDocuments().subscribe({
      next: result => {
        if (result) { this.documents = result; }
      },
      error: err => console.log(err)
    });
  }

  public approve(document: IAdmissionDocument): void {
    document.approved = true;
    document.supplier = null;
    document.targetWarehouse = null;

    this.service.updateDocument(document.id, document).subscribe({
      next: result => {
        this.toastrService.success("Document Approved!");
        this.ngOnInit()
      },
      error: err => console.log(err)
    });
  }

  public cancel(document: IAdmissionDocument): void {
    document.canceled = true;
    document.supplier = null;
    document.targetWarehouse = null;

    this.service.updateDocument(document.id, document).subscribe({
      next: result => {
        this.toastrService.warning("Document Canceled!");
        this.ngOnInit()
      },
      error: err => console.log(err)
    });
  }
}
