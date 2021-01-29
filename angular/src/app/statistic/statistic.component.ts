import { Component, Injector, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { Label } from 'ng2-charts';
import { StatisticService } from 'services/statistic.service';

@Component({
  selector: 'app-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css'],
  animations: [appModuleAnimation()],
})
export class StatisticComponent extends AppComponentBase implements OnInit {


  forAdmin: boolean;
  data: any = {};
  barchart: any = {
    data: [ {data: [], label: ''}],
    labels: []
  };
  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: 'end',
        align: 'end',
      }
    }
  };
  public barChartLabels: Label[] = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  public barChartType: ChartType = 'bar';
  public barChartLegend = true;
  public barChartPlugins = [pluginDataLabels];

  public barChartData: ChartDataSets[] = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' }
  ];
  constructor(
    injector: Injector,
    private _statisticService: StatisticService,
    private _router: ActivatedRoute
  ) {
    super(injector);
  }

  ngOnInit(): void {

    this.forAdmin = this._router.snapshot.data.forAdmin; 
    let apiCall;
    if (this.forAdmin) {
      apiCall = this._statisticService
        .getAdminDashboardData();
    } else {
      apiCall = this._statisticService
        .getDashboardData();
    }

    apiCall
      .subscribe(s => {
        this.data = s.result;
        this.barchart.labels = this.data.activityChart.data.map(s=>s.key);
        this.barchart.data = [ {data: this.data.activityChart.data.map(s=>s.value), label: this.data.activityChart.legend} ];
        console.log(this.barchart);
      });
  }

}
