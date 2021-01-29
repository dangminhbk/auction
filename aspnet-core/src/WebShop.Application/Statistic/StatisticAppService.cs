using Abp.Authorization;
using System.Threading.Tasks;
using WebShop.Authorization;
using WebShop.Domain.Statistic;

namespace WebShop.Statistic
{
    public class StatisticAppService : WebShopAppServiceBase
    {
        private readonly IStatisticDomainService _statisticDomainService;
        public StatisticAppService(IStatisticDomainService statisticDomainService)
        {
            _statisticDomainService = statisticDomainService;
        }
        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<SellerDashboard> GetDashboardData()
        {
            var sellerId = (await GetCurrentSeller()).Id;
            return await _statisticDomainService.GetSellerStatistic(sellerId);
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<AdminDashboard> GetAminDashboardData()
        {
            return await _statisticDomainService.GetAdminStatistic();
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<ReportBase> GetSellerRevenueAllTimeReport() {
            var sellerId = (await GetCurrentSeller()).Id;
            return await _statisticDomainService.GetSellerRevenueAllTimeReport(sellerId);
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<ReportBase> GetSellerRevenueByYearReport(int year)
        {
            var sellerId = (await GetCurrentSeller()).Id;
            return await _statisticDomainService.GetSellerRevenueReportByYear(sellerId, year);
        }

        [AbpAuthorize(PermissionNames.Sellers)]
        public async Task<ReportBase> GetSellerRevenueByMonthReport(int year, int month)
        {
            var sellerId = (await GetCurrentSeller()).Id;
            return await _statisticDomainService.GetSellerRevenueReportByMonth(sellerId, year, month);
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<ReportBase> GetAdminRevenueAllTimeReport()
        {
            return await _statisticDomainService.GetAdminRevenueAllTimeReport();
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<ReportBase> GetAdminRevenueByYearReport(int year)
        {
            return await _statisticDomainService.GetAdminRevenueReportByYear(year);
        }

        [AbpAuthorize(PermissionNames.Admins)]
        public async Task<ReportBase> GetAdminRevenueByMonthReport(int year, int month)
        {
            return await _statisticDomainService.GetAdminRevenueReportByMonth(year, month);
        }
    }
}
