import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IWarehouse } from '../shared/models/IWarehouse';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  private baseUrl: string = environment.apiUrl;
  private endpoint: string = environment.warehousesEndpoint;

  constructor(private http: HttpClient) { }

  public getWarehouses(): Observable<IWarehouse[]> {
    return this.http.get<IWarehouse[]>(this.baseUrl + this.endpoint);
  }
}
