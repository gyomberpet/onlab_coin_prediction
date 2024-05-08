import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Frequency } from "../models/enums/frequency";
import { IPairBTCUSDT } from "../models/pairBTCUSDT";
import { ISimulationResult } from "../models/simulationResult";

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

  public runSimulation(startStamp: number, endStamp: number, frequency: string, inputMoney: number, trainTestSplit: number): Observable<ISimulationResult> {
    return this.httpClient.get<ISimulationResult>(`${this.baseUrl}api/coinpairs/simulate`, {
      params: new HttpParams()
        .set('startstamp', startStamp)
        .set('endstamp', endStamp)
        .set('frequency', frequency)
        .set('inputmoney', inputMoney)
        .set('traintestsplit', trainTestSplit)
    });
  }


}
