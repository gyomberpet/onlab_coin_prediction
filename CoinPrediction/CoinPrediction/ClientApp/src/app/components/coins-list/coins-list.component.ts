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
  displayedColumns: string[] = ['rank', 'name', 'price'];

  constructor(private coinGeckoService: CoinGeckoService) { }


  ngOnInit() {
    this.getCoins("huf");
  }

  private getCoins(currency: string) {
    this.coinGeckoService.getMarkets(currency).subscribe(result => {
      this.coins = result;
      console.log(this.coins);

    }, error => console.log(error));
  }

}
