using Abp.Application.Services.Dto;
using System;
using WebShop.Domain.Invoice;

namespace WebShop.Invoice.Dto
{
    public class InvoiceListDto : EntityDto<long>
    {
        public DateTime CreateDate { get; set; }
        public string ProductName { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus PaymentStatus { get; set; }
        public string ShopName { get; set; }

    }
}
