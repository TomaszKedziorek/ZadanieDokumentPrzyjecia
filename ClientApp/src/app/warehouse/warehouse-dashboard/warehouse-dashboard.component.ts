import { Component, OnInit } from '@angular/core';
import { WarehouseService } from '../warehouse.service';
import { IWarehouse } from 'src/app/shared/models/IWarehouse';

@Component({
  selector: 'app-warehouse-dashboard',
  templateUrl: './warehouse-dashboard.component.html'
})
export class WarehouseDashboardComponent implements OnInit {

  public warehouses!: IWarehouse[];

  constructor(private service: WarehouseService) {
  }

  ngOnInit(): void {
    this.getWarehouses();
  }

  public getWarehouses(): void {
    this.service.getWarehouses().subscribe({
      next: result => {
        if (result) { this.warehouses = result }
      },
      error: err => console.log(err)
    });
  }

}
