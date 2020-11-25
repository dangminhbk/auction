using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop.Authorization.Users;
using WebShop.Domain.Bid;
using WebShop.Domain.Invoice;

namespace WebShop.Invoice.Dto
{
    [AutoMap(typeof(Domain.Invoice.Invoice))]
    public class InvoiceDetailDto : EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public string ProductName { get; set; }
        public string ReceiperName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public decimal SubTotal { get; set; }
        public string SerialNumber { get; set; }
        public Domain.Seller.Seller Seller { get; set; }
    }
}
