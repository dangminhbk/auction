using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Statistic
{
    public class ColumnChartBase
    {
        public string Legend { get; set; }
        public List<KeyValuePair<string,decimal>> Data { get; set; }
    }
}
