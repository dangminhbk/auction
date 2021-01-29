using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Statistic
{
    public class StatisticDomainService : DomainService, IStatisticDomainService
    {
        private readonly IRepository<Auction.Auction, long> _auctionRepository;
        private readonly IRepository<Product.Product, long> _productRepository;
        private readonly IRepository<Domain.Cash.PayIn, long> _payInRepository;
        private readonly IRepository<Bid.Bid, long> _bidRepository;
        private readonly IRepository<Invoice.Invoice, long> _invoiceRepository;
        private readonly IRepository<Seller.Seller, long> _sellerRepository;

        public StatisticDomainService(
            IRepository<Auction.Auction, long> auctionRepository,
            IRepository<Product.Product, long> productRepository,
            IRepository<Bid.Bid, long> bidRepository,
            IRepository<Invoice.Invoice, long> invoiceRepository,
            IRepository<Seller.Seller, long> sellerRepository,
            IRepository<Domain.Cash.PayIn, long> payInRepository
        )
        {
            _auctionRepository = auctionRepository;
            _productRepository = productRepository;
            _bidRepository = bidRepository;
            _invoiceRepository = invoiceRepository;
            _sellerRepository = sellerRepository;
            _payInRepository = payInRepository;
        }
        public Task GetAdminChart()
        {
            throw new NotImplementedException();
        }

        public async Task<ReportBase> GetAdminRevenueAllTimeReport()
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu toàn thời gian";
            report.Time = "";
            report.ColumnLabels = new string[] { "Năm", "Doanh thu", "Ghi chú" };

            var data = _payInRepository
                .GetAll()
                .GroupBy(
                    s => s.CreationTime.Year
                )
                .Select(
                    s => new
                    {
                        Year = s.Key,
                        Revenue = s.Sum(s => s.Money),
                    }

                ).
                ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Year.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<ReportBase> GetAdminRevenueReportByMonth(int year, int month)
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu theo tháng";
            report.Time = $"Tháng {month} năm {year}";
            report.ColumnLabels = new string[] { "Ngày", "Doanh thu", "Ghi chú" };

            var data = _payInRepository
                .GetAll()
                .Where(s => s.CreationTime.Year == year)
                .Where(s => s.CreationTime.Month == month)
                .GroupBy(
                    s => s.CreationTime.Day
                )
                .Select(
                    s => new
                    {
                        Year = s.Key,
                        Revenue = s.Sum(s => s.Money),
                    }

                ).
                ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Year.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<ReportBase> GetAdminRevenueReportByYear(int year)
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu theo năm";
            report.Time = $"Năm {year}";
            report.ColumnLabels = new string[] { "Tháng", "Doanh thu", "Ghi chú" };

            var data = _payInRepository
                .GetAll()
                .Where(s => s.CreationTime.Year == year)
                .GroupBy(
                    s => s.CreationTime.Month
                )
                .Select(
                    s => new
                    {
                        Year = s.Key,
                        Revenue = s.Sum(s => s.Money),
                    }

                ).
                ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Year.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<AdminDashboard> GetAdminStatistic()
        {
            var statistic = new AdminDashboard();

            statistic.AuctionCount =
                _auctionRepository.GetAll()
                .Count();

            statistic.ProductCount =
                _productRepository.GetAll()
                .Count();

            statistic.RevenueCount =
                _payInRepository.GetAll()
                .Sum(s => s.Money);

            statistic.SellerCount =
                _sellerRepository.GetAll()
                .Count();

            statistic.ActivityChart = new ColumnChartBase();
            statistic.ActivityChart.Legend = "Số lượt";

            var now = DateTime.Now;
            var dateFrom = DateTime.Now.AddDays(-6);

            var listData = new Dictionary<string, decimal>();
            for (int i = 6; i >= 0; i--)
            {
                var date = now.AddDays(-i).ToString("dd-MM");
                listData.Add(date, 0);
            }
            _auctionRepository
            .GetAll()
            .Where(s => s.CreationTime.Date < now && s.CreationTime.Date > dateFrom)
            .GroupBy(s => s.CreationTime.Date)
            .Select(s => new
            {

                Count = s.Count(),
                Label = s.Key
            }
            )
            .ToList()
            .ForEach(s =>
            {
                listData[s.Label.ToString("dd-MM")] = s.Count;
            });

            statistic.ActivityChart.Data = listData.ToList();
            return statistic;
        }

        public Task GetSellerChart()
        {
            throw new NotImplementedException();
        }

        public async Task<ReportBase> GetSellerRevenueAllTimeReport(long sellerId)
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu toàn thời gian";
            report.Time = "";
            report.ColumnLabels = new string[] { "Năm", "Doanh thu", "Ghi chú" };

            var data =
                _invoiceRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Where(s => s.PaymentStatus == Invoice.OrderStatus.Success)
                .GroupBy(
                    s => s.LastModificationTime.Value.Year
                ).
                Select(
                    s => new
                    {
                        Year = s.Key,
                        Revenue = s.Sum(s => s.SubTotal),
                    }

                ).
                ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Year.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<ReportBase> GetSellerRevenueReportByMonth(long sellerId, int year, int month)
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu theo tháng";
            report.Time = $"Tháng {month} năm {year}";
            report.ColumnLabels = new string[] { "Ngày", "Doanh thu", "Ghi chú" };

            var data =
                _invoiceRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Where(s => s.PaymentStatus == Invoice.OrderStatus.Success)
                .Where(s => s.LastModificationTime.Value.Year == year)
                .Where(s => s.LastModificationTime.Value.Month == month)
                .GroupBy(s => s.LastModificationTime.Value.Day)
                .Select(s => new
                {
                    Month = s.Key,
                    Revenue = s.Sum(a => a.SubTotal)
                })
                .ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Month.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<ReportBase> GetSellerRevenueReportByYear(long sellerId, int year)
        {
            var report = new ReportBase();

            report.ReportName = "Báo cáo doanh thu theo năm";
            report.Time = $"Năm {year}";
            report.ColumnLabels = new string[] { "Tháng", "Doanh thu", "Ghi chú" };

            var data =
                _invoiceRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Where(s => s.PaymentStatus == Invoice.OrderStatus.Success)
                .Where(s => s.LastModificationTime.Value.Year == year)
                .GroupBy(s => s.LastModificationTime.Value.Month)
                .Select(s => new
                {
                    Month = s.Key,
                    Revenue = s.Sum(a => a.SubTotal)
                })
                .ToList();

            report.Items = data.Select(s =>
            {
                var item = new string[2];
                item[0] = s.Month.ToString();
                item[1] = s.Revenue.ToString("c", CultureInfo.CreateSpecificCulture("vi-VN"));
                return item;
            }).ToList();

            return report;
        }

        public async Task<SellerDashboard> GetSellerStatistic(long sellerId)
        {
            var statistic = new SellerDashboard();

            statistic.AuctionCount =
                _auctionRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Count();

            statistic.ProductCount =
                _productRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Count();

            statistic.RevenueCount =
                _invoiceRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Where(s => s.PaymentStatus == Invoice.OrderStatus.Success)
                .Sum(s => s.SubTotal);

            statistic.OrderCount =
                _invoiceRepository.GetAll()
                .Where(s => s.SellerId == sellerId)
                .Count();

            return statistic;
        }
    }
}
