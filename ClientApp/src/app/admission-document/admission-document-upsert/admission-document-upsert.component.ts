import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IAdmissionDocument } from 'src/app/shared/models/IAdmissionDocument';
import { AdmissionDocumentService } from '../admission-document.service';
import { ToastrService } from 'ngx-toastr';
import { FormsModule, NgForm } from '@angular/forms';
import { ISupplier } from 'src/app/shared/models/ISupplier';
import { ILabel } from 'src/app/shared/models/ILabel';
import { IWarehouse } from 'src/app/shared/models/IWarehouse';
import { WarehouseService } from 'src/app/warehouse/warehouse.service';
import { SupplierService } from 'src/app/supplier/supplier.service';
import { CommodityService } from 'src/app/commodity/commodity.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ICommodity } from 'src/app/shared/models/ICommodity';

@Component({
  selector: 'app-admission-document-upsert',
  templateUrl: './admission-document-upsert.component.html'
})
export class AdmissionDocumentUpsertComponent implements OnInit {

  public document!: IAdmissionDocument;
  public suppliers!: ISupplier[];
  public warehouses!: IWarehouse[];
  public labels!: ILabel[];
  public commodities!: ICommodity[];
  public header!: string;
  public id!: number;
  public selectedLabelIdList: number[] = [];
  public labelsSelectedList: { label: ILabel, selected: boolean }[] = [];
  public labelsList!: ILabel[];
  public labelsDropdownSettings!: IDropdownSettings;
  public commoditiesDropdownSettings!: IDropdownSettings;

  constructor(
    private route: ActivatedRoute,
    private documentService: AdmissionDocumentService,
    private warehouseService: WarehouseService,
    private supplierService: SupplierService,
    private commodityServie: CommodityService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.getWarehouses();
    this.getSuppliers();
    this.getLabels();
    this.getCommodities();
    this.setLabelsDropdownSettings();

    if (this.id != 0) {
      this.header = "Edit"
      this.getDocumentById(this.id);
    } else {
      this.id = 0;
      this.header = "Create New";
      this.document = ({} as any) as IAdmissionDocument;
      this.setCommoditiesDropdownSettings();
    }
  }

  private setLabelsDropdownSettings(): void {
    this.labelsDropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 8,
      allowSearchFilter: false,
    };
  }

  private setCommoditiesDropdownSettings(): void {
    this.commoditiesDropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 10,
      allowSearchFilter: false,
    };
  }


  private getDocumentById(id: number) {
    this.documentService.getDocumentById(id).subscribe({
      next: result => { this.document = result; },
      error: err => console.log(err)
    });
  }

  private getLabels() {
    this.documentService.getLabels().subscribe({
      next: result => { this.labels = result; },
      error: err => console.log(err)
    });
  }

  private getWarehouses() {
    this.warehouseService.getWarehouses().subscribe({
      next: result => { this.warehouses = result; },
      error: err => console.log(err)
    });
  }

  private getSuppliers() {
    this.supplierService.getSuppliers().subscribe({
      next: result => { this.suppliers = result; },
      error: err => console.log(err)
    });
  }

  private getCommodities() {
    this.commodityServie.getCommodities().subscribe({
      next: result => {
        this.commodities = result.
          filter(res => res.admissionDocumentId == null);
      },
      error: err => console.log(err)
    });
  }

  public onSubmit(form: NgForm): void {
    let admissionDoc: IAdmissionDocument = {
      id: form.value.id,
      targetWarehouseId: Number(form.value.targetWarehouseId),
      supplierId: Number(form.value.supplierId),
      approved: form.value.approved,
      canceled: form.value.canceled,
      targetWarehouse: null,
      supplier: null,
      labels: form.value.labels,
      commodityList: form.value.commodities
    }
    if (this.id != 0) {

      this.updateDocument(this.id, admissionDoc);
    } else {
      this.createDocument(admissionDoc);
    }
  }

  private updateDocument(id: number, commodity: IAdmissionDocument): void {
    this.documentService.updateDocument(id, commodity).subscribe({
      next: result => {
        this.toastrService.success("Updated succesfully!");
        this.router.navigate(['/admission-documents']);
      },
      error: err => {
        console.log(err)
      }
    });
  }

  private createDocument(commodity: IAdmissionDocument): void {
    this.documentService.createDocument(commodity).subscribe({
      next: result => {
        this.toastrService.success("Created succesfully!");
        this.router.navigate(['/admission-documents']);
      },
      error: err => {
        console.log(err)
      }
    });
  }
}
