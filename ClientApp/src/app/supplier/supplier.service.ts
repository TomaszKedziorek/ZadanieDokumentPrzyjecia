import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISupplier } from '../shared/models/ISupplier';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  private baseUrl: string = environment.apiUrl;
  private endpoint: string = environment.suppliersEndpoint;

  constructor(private http: HttpClient) { }

  public getSuppliers(): Observable<ISupplier[]> {
    return this.http.get<ISupplier[]>(this.baseUrl + this.endpoint);
  }

  public getSupplierById(id: number): Observable<ISupplier> {
    return this.http.get<ISupplier>(this.baseUrl + this.endpoint + id);
  }

  public updateSupplier(id: number, supplier: ISupplier) {
    return this.http.put<ISupplier>(this.baseUrl + this.endpoint + id, supplier);
  }

  public createSupplier(supplier: ISupplier) {
    return this.http.post<ISupplier>(this.baseUrl + this.endpoint, supplier);
  }

  public deleteSupplier(id: number) {
    return this.http.delete<ISupplier>(this.baseUrl + this.endpoint + id);
  }
}
