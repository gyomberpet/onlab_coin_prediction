import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ICoin } from "../models/Coin";
import { ICoinMarket } from "../models/CoinMarket";
import { Currency } from "../models/enums/currency";
import { IUser } from "../models/user";
import { CoinGeckoService } from "./coin-gecko.service";
import { UserService } from "./user.service";

@Injectable({
  providedIn: 'root'
})
export class BalanceService {

  private baseUrl: string;

  constructor(private userService: UserService, private coinGeckoService: CoinGeckoService) { }

  public getBalance(userId: number): Observable<number> {
    const obs = new Observable<number>(observable => {
      this.userService.getUserById(userId).subscribe(result => {
        var user = result;
        var coinIds = user.userAssets.map(a => a.coin.coinId);
        this.coinGeckoService.getMarkets(Currency.USD, coinIds).subscribe(result => {
          var balance = this.calculateBalance(user, result);
          observable.next(balance);
        }), error => console.log(error);
      }), error => console.log(error);
    });
    return obs;
  }

  private calculateBalance(user: IUser, markets: ICoinMarket[]): number {
    var balance = 0;
    user.userAssets.forEach(asset => {
      var coin: ICoinMarket | undefined = markets.find(m => m.id == asset.coin.coinId);
      if (!coin) {
        coin = {
          id: '',
          symbol: '',
          name: '',
          currentPrice: 0
        };
      }
      var price = coin.currentPrice;
      balance += price * asset.amount;
    })
    return balance;
  }
}
