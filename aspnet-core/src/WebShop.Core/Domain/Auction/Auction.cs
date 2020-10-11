using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Auction
{
    public class Auction : FullAuditedAggregateRoot<long>
    {
    }
}
