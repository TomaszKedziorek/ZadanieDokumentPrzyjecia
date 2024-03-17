import { Component, OnInit } from '@angular/core';
import { SupplierService } from '../supplier.service';
import { ISupplier } from 'src/app/shared/models/ISupplier';

@Component({
  selector: 'app-supplier-dashboard',
  templateUrl: './supplier-dashboard.component.html'
})
export class SupplierDashboardComponent implements OnInit {

  public suppliers!: ISupplier[];

  constructor(private service: SupplierService) {
  }

  ngOnInit(): void {
    this.getSuppliers();
  }

  private getSuppliers(): void {
    this.service.getSuppliers().subscribe({
      next: result => this.suppliers = result,
      error: err => console.log(err)
    })
  }



}
