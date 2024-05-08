import { IUserAsset } from "./UserAsset";

export interface IUser {
  id?: number;
  userName: string;
  password: string;
  userAssets: IUserAsset[];
}
