import { Component, OnInit } from '@angular/core';
import { ICommodity } from '../../shared/models/ICommodity';
import { ActivatedRoute, Router } from '@angular/router';
import { CommodityService } from '../commodity.service';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-commodity-upsert',
  templateUrl: './commodity-upsert.component.html'
})
export class CommodityUpsertComponent implements OnInit {

  public commodity!: ICommodity;
  public header!: string;
  public id!: number;
  public returnUrl!: string | null;

  constructor(
    private route: ActivatedRoute,
    private service: CommodityService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.returnUrl = this.getReturnUrl();

    if (this.id != 0) {
      this.header = "Edit"
      this.getCommodityById(this.id);
    } else {
      this.id = 0;
      this.header = "Create New";
      this.commodity = ({} as any) as ICommodity;
      let documentId = Number(this.route.snapshot.queryParamMap.get("document-id"));
      if (documentId != 0)
        this.commodity.admissionDocumentId = documentId;
    }
  }

  private getReturnUrl(): string {
    let url = this.route.snapshot.queryParamMap.get('returnUrl');
    return url ? url : '/commodities'
  }

  private getCommodityById(id: number) {
    this.service.getCommodityById(id).subscribe({
      next: result => { this.commodity = result; },
      error: err => console.log(err)
    });
  }

  public onSubmit(form: NgForm): void {
    if (this.id != 0) {
      this.updateCommodity(this.id, form.value);
    } else {
      this.createCommodity(form.value);
    }
  }

  private updateCommodity(id: number, commodity: ICommodity): void {
    this.service.updateCommodity(id, commodity).subscribe({
      next: result => {
        this.toastrService.success("Updated succesfully!");
        this.router.navigate([this.returnUrl]);
      },
      error: err => {
        console.log(err)
      }
    });
  }

  private createCommodity(commodity: ICommodity): void {
    this.service.createCommodity(commodity).subscribe({
      next: result => {
        this.toastrService.success("Created succesfully!");
        this.router.navigate([this.returnUrl]);
      },
      error: err => {
        console.log(err)
      }
    });
  }

  public deleteCommodity(): void {
    if (this.id != 0) {
      this.service.deleteCommodity(this.id).subscribe({
        next: result => {
          this.toastrService.success("Deleted succesfully!");
          this.router.navigate([this.returnUrl]);
        },
        error: err => {
          console.log(err)
        }
      });
    }
  }

}
