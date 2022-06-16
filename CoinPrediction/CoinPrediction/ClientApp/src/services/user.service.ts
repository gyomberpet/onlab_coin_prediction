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

  /**
   * Get a user with the given id.
   * @param id
   */
  public getUserById(id: number): Observable<IUser> {
    return this.httpClient.get<IUser>(`${this.baseUrl}api/users/${id}`);
  }

  /**
   * Insert a new user to the db.
   * @param user
   */
  public insertUser(user: IUser): Observable<IUser> {
    return this.httpClient.post<IUser>(`${this.baseUrl}api/users`, user);
  }

  /**
   * Add the given asset to the user.
   * @param userId
   * @param asset
   */
  public addAssetToUser(userId: number, asset: IUserAsset): Observable<IUser> {
    return this.httpClient.put<IUser>(`${this.baseUrl}api/users/${userId}`, asset);
  }

}
