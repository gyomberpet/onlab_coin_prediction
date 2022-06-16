import { Component, Input, OnChanges } from '@angular/core';
import { Color, ScaleType } from '@swimlane/ngx-charts';
import { INgxChartData } from '../../../models/ngxChartData';

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

  /**
   * Set the scaling parameters.
   * */
  ngOnChanges(): void {
    if (!this.data) return;

    var prices = this.data[0].series.map(p => p.value);
    var maxPrice = Math.max(...prices);
    var minPrice = Math.min(...prices);
    this.yScaleMax = maxPrice + (maxPrice - minPrice) * 0.15;
    this.yScaleMin = minPrice - (maxPrice - minPrice) * 0.25;
  }

  onSelect(data: any): void {}

  onActivate(data: any): void {}

  onDeactivate(data: any): void {}

  

}
