import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ICoin } from "../models/Coin";

@Injectable({
  providedIn: 'root'
})
export class CoinService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  /**
   * Return the coin with the given id.
   * @param id
   */
  public getCoinById(id: number): Observable<ICoin> {
    return this.httpClient.get<ICoin>(`${this.baseUrl}api/coins/${id}`);
  }

}
