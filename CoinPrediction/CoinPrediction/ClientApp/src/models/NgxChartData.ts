export interface INgxChartData {
  name: string;
  series: ITimestampValuePair[];
}

export interface ITimestampValuePair {
  name: string;
  value: number;
}
