using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebShop.Authorization.Users;

namespace WebShop.Seller
{
    public class Seller : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Seller.User))]
        public long UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Product.Product> Products { get; set; }
    }
}
