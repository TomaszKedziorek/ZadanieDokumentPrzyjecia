import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAdmissionDocument } from '../shared/models/IAdmissionDocument';
import { Observable, map, scheduled, asyncScheduler } from 'rxjs';
import { ILabel } from '../shared/models/ILabel';

@Injectable({
  providedIn: 'root'
})
export class AdmissionDocumentService {

  private baseUrl: string = environment.apiUrl;
  private endpoint: string = environment.admissionDocumentsEndpoint;

  constructor(private http: HttpClient) { }

  public getDocuments(): Observable<IAdmissionDocument[]> {
    return this.http.get<IAdmissionDocument[]>(this.baseUrl + this.endpoint)
      .pipe(
        map(doc => doc.filter(x => x.canceled == false))
      );
  }

  public getDocumentById(id: number): Observable<IAdmissionDocument> {
    return this.http.get<IAdmissionDocument>(this.baseUrl + this.endpoint + id);
  }

  public updateDocument(id: number, document: IAdmissionDocument) {
    return this.http.put<IAdmissionDocument>(this.baseUrl + this.endpoint + id, document);
  }

  public createDocument(document: IAdmissionDocument) {
    return this.http.post<IAdmissionDocument>(this.baseUrl + this.endpoint, document);
  }

  public getLabels(): Observable<ILabel[]> {
    return this.http.get<ILabel[]>(this.baseUrl + environment.lablesEndpoint);
  }
}
