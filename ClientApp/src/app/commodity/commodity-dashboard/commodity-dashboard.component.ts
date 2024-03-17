import { Component, OnDestroy, OnInit } from '@angular/core';
import { ICommodity } from '../../shared/models/ICommodity';
import { CommodityService } from '../commodity.service';

@Component({
  selector: 'app-commodity-dashboard',
  templateUrl: './commodity-dashboard.component.html'
})
export class CommodityDashboardComponent implements OnInit {

  public commodities!: ICommodity[];

  constructor(private service: CommodityService) {
  }

  ngOnInit(): void {
    this.getCommodities();
  }

  private getCommodities(): void {
    this.service.getCommodities().subscribe({
      next: result => {
        if (result) { this.commodities = result }
      },
      error: err => console.log(err)
    });
  }
}