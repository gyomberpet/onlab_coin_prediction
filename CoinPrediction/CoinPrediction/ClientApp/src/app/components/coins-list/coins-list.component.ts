import { Component, Inject, OnInit } from '@angular/core';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { CoinGeckoService } from '../../../services/coin-gecko.service';

@Component({
  selector: 'app-coins-list',
  templateUrl: './coins-list.component.html',
  styleUrls: ['./coins-list.component.css']
})
export class CoinsListComponent implements OnInit {

  coins: ICoinMarket[] = [];
  displayedColumns: string[] = ['rank', 'image', 'name', 'symbol', 'price', '1h', '24h', '7d'];
  selectedCurrencyType: Currency = Currency.USD;

  constructor(private coinGeckoService: CoinGeckoService) { }


  ngOnInit() {
    this.getCoins(this.selectedCurrencyType);
  }

  private getCoins(currency: string) {
    this.coinGeckoService.getMarkets(currency).subscribe(result => {
      this.coins = result;
    }, error => console.log(error));
  }

}

