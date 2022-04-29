import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { IUser } from "../models/user";
import { IUserAsset } from "../models/UserAsset";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getUserById(id: number): Observable<IUser> {
    return this.httpClient.get<IUser>(`${this.baseUrl}api/users/${id}`);
  }

  public insertUser(user: IUser): Observable<IUser> {
    return this.httpClient.post<IUser>(`${this.baseUrl}api/users`, user);
  }

  public addAssetToUser(userId: number, asset: IUserAsset): Observable<IUser> {
    return this.httpClient.put<IUser>(`${this.baseUrl}api/users/${userId}`, asset);
  }

}
