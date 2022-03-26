import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoinMarket } from '../models/CoinMarket';
import { IPing } from '../models/Ping';

@Injectable({
  providedIn: 'root'
})
export class CoinGeckoService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public ping(): Observable<IPing>{
    return this.httpClient.get<IPing>(`${this.baseUrl}api/coingecko/ping`);
  }

  public getMarkets(currency: string): Observable<ICoinMarket[]> {
    return this.httpClient.get<ICoinMarket[]>(`${this.baseUrl}api/coingecko/coinMarkets/${currency}`);
  }
}
