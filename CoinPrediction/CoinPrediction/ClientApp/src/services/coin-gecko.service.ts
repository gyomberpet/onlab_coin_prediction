import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ICoinMarket } from '../models/CoinMarket';
import { IMarketChart } from '../models/MarketChart';
import { IPing } from '../models/Ping';

@Injectable({
  providedIn: 'root'
})
export class CoinGeckoService {

  private baseUrl: string;

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  /**
   * Ping to check the server is available.
   * */
  public ping(): Observable<IPing>{
    return this.httpClient.get<IPing>(`${this.baseUrl}api/coingecko/ping`);
  }

  /**
   * Get the given coin with the given currency.
   * @param id
   * @param currency
   */
  public getCoin(id: string, currency: string): Observable<ICoinMarket> {
    return this.httpClient.get<ICoinMarket>(`${this.baseUrl}api/coingecko/coin/${id}/${currency}`);
  }

  /**
   * Get the markets.
   * @param currency
   * @param coins
   */
  public getMarkets(currency: string, coins?: string[]): Observable<ICoinMarket[]> {
    return this.httpClient.put<ICoinMarket[]>(`${this.baseUrl}api/coingecko/coinmarkets/${currency}`, coins ?? []);
  }

  /**
   * Get the market chart data.
   * @param id
   * @param currency
   * @param days
   * @param interval
   */
  public getMarketChart(id: string, currency: string, days: string, interval: string): Observable<IMarketChart> {
    return this.httpClient.get<IMarketChart>(`${this.baseUrl}api/coingecko/marketchart/${id}/${currency}/${days}/${interval}`);
  }
}
