import { IBaseEntity } from "./IBaseEntity";
import { ICommodity } from "./ICommodity";
import { ILabel } from "./ILabel";
import { ISupplier } from "./ISupplier";
import { IWarehouse } from "./IWarehouse";

export interface IAdmissionDocument extends IBaseEntity {
  targetWarehouseId: number | null;
  targetWarehouse: IWarehouse | null;
  supplierId: number | null;
  supplier: ISupplier | null;
  labels: ILabel[];
  commodityList: ICommodity[];
  canceled: boolean;
  approved: boolean;
}