using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace WebShop.Domain.Statistic
{
    public class ReportBase
    {
        public string ReportName { get; set; }
        public string Time { get; set; }
        public string[] ColumnLabels { get; set; }
        public List<string[]> Items { get; set; }
    }

}
