import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICommodity } from '../shared/models/ICommodity';

@Injectable({
  providedIn: 'root'
})
export class CommodityService {

  private baseUrl: string = environment.apiUrl;
  private endpoint: string = environment.commoditiesEndpoint;

  constructor(private http: HttpClient) { }

  public getCommodities(): Observable<ICommodity[]> {
    return this.http.get<ICommodity[]>(this.baseUrl + this.endpoint);
  }

  public getCommodityById(id: number): Observable<ICommodity> {
    return this.http.get<ICommodity>(this.baseUrl + this.endpoint + id);
  }

  public updateCommodity(id: number, commodity: ICommodity) {
    return this.http.put(this.baseUrl + this.endpoint + id, commodity);
  }

  public createCommodity(commodity: ICommodity) {
    return this.http.post(this.baseUrl + this.endpoint, commodity);
  }

  public deleteCommodity(id: number) {
    return this.http.delete(this.baseUrl + this.endpoint + id);
  }
}
