using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.CashFlow
{
    public class CashIn : FullAuditedAggregateRoot<long>
    {
        public long OrderId { get; set; }
        public decimal MoneyAmount { get; set; }
        public DateTime CashInDate { get; set; }
        public string MomoId { get; set; }
    }
}
