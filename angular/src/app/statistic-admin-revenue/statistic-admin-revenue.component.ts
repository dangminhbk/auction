import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { StatisticService } from 'services/statistic.service';
import { ReportDto } from '../statistic-order/dto/report-dto';
import * as printJS from "print-js";
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-statistic-admin-revenue',
  templateUrl: './statistic-admin-revenue.component.html',
  styleUrls: ['./statistic-admin-revenue.component.css'],
  animations: [appModuleAnimation()]
})
export class StatisticAdminRevenueComponent extends PagedListingComponentBase<any> implements OnInit {

  report: ReportDto = new ReportDto;
  month = "2020-12";
  year = 2020;
  dataLoadDone = false;
  reportName =  [
    { name: "Báo cáo toàn thời gian", val: 1 },
    { name: "Báo cáo theo tháng", val: 2 },
    { name: "Báo cáo theo năm", val: 3 },
    // { name: "Báo cáo theo sản phẩm", val: 4 }
  ]

  currentReport = 1;
  
  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
  }
  protected delete(entity: any): void {
    throw new Error('Method not implemented.');
  }

  constructor(
    injector: Injector,
    private _statisticService: StatisticService,
    private _router: ActivatedRoute
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

  loadData() {
    console.log(this.month);
    this.dataLoadDone = false;
    abp.ui.setBusy();
    let apiCall;
    switch(this.currentReport) {
      case 1: {
        apiCall = this._statisticService.getAdminReportAllTime();
        break;
      }

      case 2: {
        const month = parseInt(this.month.substring(5,7));
        const year = parseInt(this.month.substring(0,4));
        apiCall = this._statisticService.getAdminReportByMonth(year, month);
        break;
      }

      case 3: {
        apiCall = this._statisticService.getAdminReportByYear(this.year);
        break;
      }
    }

    apiCall.subscribe(s=> {
      this.report = s.result;
      abp.ui.clearBusy();
      this.dataLoadDone = true;
    })
  }

  print() {
    html2canvas(document.querySelector('#print')).then(async (canvas: HTMLCanvasElement) => {
      const toImg = canvas.toDataURL();

      /**
       * Print image
       *
       * Look at the
       * {@Link https://github.com/crabbly/print.js}
       */
      printJS({printable: `${toImg}`, type: 'image', imageStyle: 'width:100%'});
    });  }

}
