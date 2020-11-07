using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.CashFlow
{
    public class CashOut : FullAuditedAggregateRoot<long>
    {
        public long SellerId { get; set; }
        public decimal MoneyAmount { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountHolder { get; set; }
        public DateTime TranferedDate { get; set; }
    }
}
