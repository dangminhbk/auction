using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Product;

namespace WebShop.Category
{
    public class Category : FullAuditedAggregateRoot<long>
    {
        public string Name { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
