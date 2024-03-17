import { IBaseEntity } from "./IBaseEntity";

export interface ICommodity extends IBaseEntity {
  name: string
  code: string
  price: number;
  quantity: number;
  admissionDocumentId: number | null;
}