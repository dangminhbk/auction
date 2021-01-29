using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.PayIn.Dto
{
    public class CreditHistory : EntityDto<long>
    {
        public DateTime Date { get; set; }
        public string SellerName { get; set; }
        public decimal Money { get; set; }
        public long Credit { get; set; }
    }
}
