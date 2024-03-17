import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierService } from '../supplier.service';
import { ToastrService } from 'ngx-toastr';
import { ISupplier } from 'src/app/shared/models/ISupplier';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-supplier-upsert',
  templateUrl: './supplier-upsert.component.html'
})
export class SupplierUpsertComponent implements OnInit {

  public supplier!: ISupplier;
  public header!: string;
  public id!: number;

  constructor(
    private route: ActivatedRoute,
    private service: SupplierService,
    private toastrService: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    if (this.id != 0) {
      this.header = "Edit"
      this.getSupplierById(this.id);
    } else {
      this.id = 0;
      this.header = "Create New";
      this.supplier = ({} as any) as ISupplier;
    }
  }

  private getSupplierById(id: number): void {
    this.service.getSupplierById(id).subscribe({
      next: result => { this.supplier = result; console.log(result) },
      error: err => console.log(err)
    });
  }

  public onSubmit(form: NgForm): void {
    if (this.id != 0) {
      this.updateSupplier(this.id, form.value);
    } else {
      this.createSupplier(form.value);
    }
  }

  private updateSupplier(id: number, supplier: ISupplier): void {
    this.service.updateSupplier(id, supplier).subscribe({
      next: result => {
        this.toastrService.success("Updated succesfully!");
        this.router.navigate(['/suppliers']);
        console.log(result);
      },
      error: err => {
        console.log(err)
      }
    });
  }

  private createSupplier(supplier: ISupplier): void {
    this.service.createSupplier(supplier).subscribe({
      next: result => {
        this.toastrService.success("Created succesfully!");
        this.router.navigate(['/suppliers']);
        console.log(result);
      },
      error: err => {
        console.log(err)
      }
    });
  }

  public deleteSupplier(): void {
    if (this.id != 0) {
      this.service.deleteSupplier(this.id).subscribe({
        next: result => {
          this.toastrService.success("Deleted succesfully!");
          this.router.navigate(['/suppliers']);
        },
        error: err => {
          console.log(err)
        }
      });
    }
  }
}
