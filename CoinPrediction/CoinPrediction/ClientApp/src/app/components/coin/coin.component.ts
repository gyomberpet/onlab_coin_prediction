import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ICoinMarket } from '../../../models/CoinMarket';
import { Currency } from '../../../models/enums/currency';
import { Frequency } from '../../../models/enums/Frequency';
import { Interval } from '../../../models/enums/Interval';
import { IMarketChart } from '../../../models/MarketChart';
import { INgxChartData, ITimestampValuePair } from '../../../models/NgxChartData';
import { CoinGeckoService } from '../../../services/coin-gecko.service';
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { SimulationEditComponent } from '../simulation-edit/simulation-edit.component';

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
  intervalControl = new FormControl();
  interval: string = Interval.DAYS_30;

  constructor(private coinGeckoService: CoinGeckoService, private route: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params.id;
    this.coinGeckoService.getCoin(this.id, Currency.USD).subscribe(result => {
      this.coin = result;
      this.updateChart(this.interval);
    }, error => console.log(error));    
  }

  private convertIMarketChartToINgxChartData(): INgxChartData[]
  {
    var series: ITimestampValuePair[] = []
    this.coinMarketChart.prices.forEach(elemet => {
      var date = this.formatDate(elemet[0])
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

  updateChart(interval: string)
  {
    this.interval = interval;
    var frequency: string;
    if (this.interval === Interval.HOURS_24 || this.interval === Interval.DAYS_7)
      frequency = Frequency.MINTUTELY;
    else if (this.interval === Interval.DAYS_14 || this.interval === Interval.DAYS_30)
      frequency = Frequency.HOURLY;
    else
      frequency = Frequency.DAILY;

    this.coinGeckoService.getMarketChart(this.id, Currency.USD, this.interval, frequency).subscribe(result => {
      this.coinMarketChart = result;
      this.dataSource = this.convertIMarketChartToINgxChartData();
    }, error => console.log(error));
  }

  formatDate(timeStamp: any): string {
    var date = new Date(timeStamp);
    var result: string;
    if (this.interval === Interval.HOURS_24 || this.interval === Interval.DAYS_7 || this.interval === Interval.DAYS_14 || this.interval === Interval.DAYS_30)
      result = `${this.getMonth(date.getMonth())} ${date.getDate() < 10 ? 0 : ''}${date.getDate()} ${date.getHours() < 10 ? 0 : ''}${date.getHours()}:${date.getMinutes() < 10 ? 0 : ''}${date.getMinutes()}`
    else
      result = `${this.getMonth(date.getMonth())} ${date.getDate() < 10 ? 0 : ''}${date.getDate()}, ${date.getFullYear()}`

    return result;
  }

  getMonth(num: number)
  {
    const month = [
      "Jan.",
      "Feb.",
      "Mar.",
      "Apr.",
      "May.",
      "Jun.",
      "Jul.",
      "Aug.",
      "Sep.",
      "Oct.",
      "Nov.",
      "Dec."
    ]

    return month[num];
  }

  openSimulationEditDialog() {
    var dialogConfig = new MatDialogConfig();
    dialogConfig.width = '500px';
    dialogConfig.data = {};
    const dialogRef = this.dialog.open(SimulationEditComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  save() {
    //this.dialogRef.close();
  }

  close() {
   // this.dialogRef.close();
  }
}
