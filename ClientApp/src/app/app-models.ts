export interface Business {
  businessId: number;
  name: string;
  productGroups: [];
}

export interface ProductGroups {
  productGroupId: number;
  name: string;
  businessId: number;
}
