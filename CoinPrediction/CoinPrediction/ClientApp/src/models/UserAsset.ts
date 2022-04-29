import { ICoin } from "./Coin";

export interface IUserAsset {
  id?: number;
  coin: ICoin;
  amount: number;
}
