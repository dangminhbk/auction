using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebShop.Domain.Cash
{
    public class PayIn : FullAuditedAggregateRoot<long>
    {
        public long Credit { get; set; }
        public decimal Money { get; set; }
        [ForeignKey(nameof(PayIn.Target))]
        public long TargetId { get; set; }
        public Seller.Seller Target { get; set; }
        public static PayIn MakePayIn(long TargetId, decimal Money)
        {
            var payIn = new PayIn();
            payIn.TargetId = TargetId;

            payIn.Money = Money;

            if (Money < 100000)
            {
                throw new UserFriendlyException("Số tiền nạp tối thiểu 100000VND");
            }

            payIn.Credit = (long)Math.Floor(Money / 3000);
            payIn.Money = Money;

            return payIn;
        }
    }
}
