import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Frequency } from "../models/enums/frequency";
import { IPairBTCUSDT } from "../models/pairBTCUSDT";

@Injectable({
  providedIn: 'root'
})
export class CoinPairService {
  private baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public getCoinPairsHourly(coinSymbol: string, frquency: Frequency): Observable<IPairBTCUSDT> {
    return this.httpClient.get<IPairBTCUSDT>(`${this.baseUrl}api/coinpairs/${coinSymbol}/${frquency}`);
  }


}
