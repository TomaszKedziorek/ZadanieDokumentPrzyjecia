import { IBaseEntity } from "./IBaseEntity";

export interface ISupplier extends IBaseEntity {
  companyName: string
  addressId: number
  country: string
  street: string
  city: string
  state: string
  zipCode: string
}