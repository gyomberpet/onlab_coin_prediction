import { Component, Input, OnChanges } from '@angular/core';
import { Color, ScaleType } from '@swimlane/ngx-charts';
import { INgxChartData } from '../../../models/NgxChartData';

@Component({
  selector: 'app-coin-chart',
  templateUrl: './coin-chart.component.html',
  styleUrls: ['./coin-chart.component.css']
})
export class CoinChartComponent implements OnChanges {

  @Input() data: INgxChartData[];

  // options
  legend: boolean = true;
  colorScheme: string = 'horizon';
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = '';
  yAxisLabel: string = 'Price';
  timeline: boolean = true;
  rotateXAxisTicks: boolean = true;
  yScaleMin: number;
  yScaleMax: number ;

  constructor() { }

  ngOnChanges(): void {
    if (!this.data) return;

    var prices = this.data[0].series.map(p => p.value);
    var maxPrice = Math.max(...prices);
    var minPrice = Math.min(...prices);
    this.yScaleMax = maxPrice + (maxPrice - minPrice) * 0.15;
    this.yScaleMin = minPrice - (maxPrice - minPrice) * 0.25;
    console.log(this.yScaleMin);
    console.log(this.yScaleMax);
  }

  onSelect(data: any): void {
    // console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data: any): void {
    // console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data: any): void {
    // console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  

}
