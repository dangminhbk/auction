using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Statistic
{
    public class AdminDashboard
    {
        public long SellerCount { get; set; }
        public long ProductCount { get; set; }
        public long AuctionCount { get; set; }
        public decimal RevenueCount { get; set; }
        public ColumnChartBase ActivityChart { get; set; }
    }
}
