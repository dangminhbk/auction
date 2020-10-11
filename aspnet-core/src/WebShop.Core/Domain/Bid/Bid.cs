using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Domain.Bid
{
    public class Bid : FullAuditedAggregateRoot<long>
    {
    }
}
