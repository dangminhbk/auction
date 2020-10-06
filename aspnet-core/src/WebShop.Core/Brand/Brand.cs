using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Brand
{
    public class Brand : FullAuditedAggregateRoot<long>
    {
        public string Name { get; set; }
        public virtual ICollection<Product.Product> Products { get; set; }
    }
}
