using Abp.Application.Services.Dto;

namespace WebShop.Invoice.Dto
{
    public class CreateInvoiceDto : EntityDto<long>
    {
        public string ProductName { get; set; }
        public decimal SubTotal { get; set; }
        public long AuctionId { get; set; }
        public string SerialNumber { get; set; }
    }
}
