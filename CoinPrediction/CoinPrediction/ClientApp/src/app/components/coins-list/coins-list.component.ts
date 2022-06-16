import { AfterViewInit } from '@angular/core';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { CoinGeckoService } from '../../../services/coin-gecko.service';


@Component({
  selector: 'app-coins-list',
  templateUrl: './coins-list.component.html',
  styleUrls: ['./coins-list.component.css']
})
export class CoinsListComponent implements OnInit, AfterViewInit {

  coins: MatTableDataSource<ICoinMarket> = new MatTableDataSource();
  displayedColumns: string[] = ['marketCapRank', 'image', 'name', 'symbol', 'price', '1h', '24h', '7d'];
  selectedCurrencyType: Currency = Currency.USD;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private coinGeckoService: CoinGeckoService) { }

  /**
   * Set the sort property.
   * */
  ngAfterViewInit(): void {
    this.coins.data = [];
    this.coins.sort = this.sort;
  }

  /**
   * Get the coins.
   * */
  ngOnInit() {
    this.getCoins(this.selectedCurrencyType);
  }

  /**
   * Download the list of coins with the given currency.
   * @param currency
   */
  private getCoins(currency: string) {
    this.coinGeckoService.getMarkets(currency).subscribe(result => {
      this.coins.data = result;
      this.coins.sort = this.sort;
    }, error => console.log(error));
  }

}

