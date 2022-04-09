import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { Frequency } from '../../../models/enums/frequency';
import { Interval } from '../../../models/enums/interval';
import { IMarketChart } from '../../../models/MarketChart';
import { INgxChartData, ITimestampValuePair } from '../../../models/NgxChartData';
import { CoinGeckoService } from '../../../services/coin-gecko.service';

@Component({
  selector: 'app-coin',
  templateUrl: './coin.component.html',
  styleUrls: ['./coin.component.css']
})
export class CoinComponent implements OnInit {

  id: string;
  coin: ICoinMarket = {
    id: '',
    symbol: '',
    name: ''
  };
  dataSource: INgxChartData[];
  coinMarketChart: IMarketChart;

  constructor(private coinGeckoService: CoinGeckoService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params.id;
    this.coinGeckoService.getCoin(this.id, Currency.USD).subscribe(result => {
      this.coin = result;
      this.coinGeckoService.getMarketChart(this.id, Currency.USD, Interval.DAYS_30, Frequency.DAILY).subscribe(result => {
        this.coinMarketChart = result;
        this.dataSource = this.convertIMarketChartToINgxChartData();
      }, error => console.log(error));
    }, error => console.log(error));    
  }

  private convertIMarketChartToINgxChartData(): INgxChartData[]
  {
    var series: ITimestampValuePair[] = []
    this.coinMarketChart.prices.forEach(elemet => {
      var date = new Date(elemet[0]).toLocaleDateString("en-us", { year:'numeric', month: 'long', day:'numeric' })
      var pair: ITimestampValuePair = {
        name: date,
        value: elemet[1]
      }
      series.push(pair);
    })
    var result: INgxChartData[] = [{
      name: this.coin.name,
      series: series
    }];

    return result;
  }
}
