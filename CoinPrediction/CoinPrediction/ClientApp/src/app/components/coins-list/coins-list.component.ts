import { Component, Inject, OnInit } from '@angular/core';
import { ICoinMarket } from '../../../models/CoinMarket';
import { CoinGeckoService } from '../../../services/coin-gecko.service';

@Component({
  selector: 'app-coins-list',
  templateUrl: './coins-list.component.html',
  styleUrls: ['./coins-list.component.css']
})
export class CoinsListComponent implements OnInit {

  coins: ICoinMarket[] = [];
  displayedColumns: string[] = ['rank', 'image', 'name', 'symbol', 'price', '1h', '24h', '7d'];
  selectedCurrencyType: CurrencyType = CurrencyType.USD;

  constructor(private coinGeckoService: CoinGeckoService) { }


  ngOnInit() {
    this.getCoins(this.selectedCurrencyType);
  }

  private getCoins(currency: string) {
    this.coinGeckoService.getMarkets(currency).subscribe(result => {
      this.coins = result;
      console.log(this.coins);

    }, error => console.log(error));
  }

}

enum CurrencyType {
  HUF = "huf",
  USD = "usd"
}
