using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Statistic
{
    public interface IStatisticDomainService : IDomainService
    {
        public Task<AdminDashboard> GetAdminStatistic();
        public Task GetAdminChart();
        public Task<SellerDashboard> GetSellerStatistic(long sellerId);
        public Task GetSellerChart();

        public Task<ReportBase> GetSellerRevenueReportByMonth(long sellerId, int year, int month);
        public Task<ReportBase> GetSellerRevenueReportByYear (long sellerId, int year);
        public Task<ReportBase> GetSellerRevenueAllTimeReport(long sellerId);


        public Task<ReportBase> GetAdminRevenueReportByMonth(int year, int month);
        public Task<ReportBase> GetAdminRevenueReportByYear(int year);
        public Task<ReportBase> GetAdminRevenueAllTimeReport();
    }
}
