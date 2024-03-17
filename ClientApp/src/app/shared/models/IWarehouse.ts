import { IBaseEntity } from "./IBaseEntity";

export interface IWarehouse extends IBaseEntity {
  name: string
  symbol: string
}