export interface PolicyModel {
  PoliceId?: number;
  Name: string;
  Description: string;
  PolicyTypeId: number;
  PolicyType?: string;
  EffectiveDate: Date;
  Terms: number;
  Cost: number;
  RiskTypeId: number;
  RiskType?: string;
  UserId?: number;
}
