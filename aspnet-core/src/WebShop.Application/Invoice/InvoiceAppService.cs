using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Invoice;
using WebShop.Invoice.Dto;
using System.Linq;

namespace WebShop.Invoice
{
    public class InvoiceAppService : WebShopAppServiceBase
    {
        private readonly IInvoiceManager _invoiceManager;
        public InvoiceAppService(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }
        public async Task Create(CreateInvoiceDto input)
        {
            await _invoiceManager.Create(
                input.ProductName,
                input.SubTotal,
                input.AuctionId,
                input.SerialNumber
            );
        }

        public async Task UpdateAddress(UpdateInvoiceAddress input)
        {
            await _invoiceManager.UpdateAddress(
                input.Id,
                input.Address,
                input.phoneNumber,
                input.ReceiperName
            );
        }

        public async Task CompleteInvoice(EntityDto<long> input)
        {
            await _invoiceManager.ChangeStatus(input.Id, OrderStatus.Success);
        }

        public async Task<PagedResultDto<InvoiceListDto>> GetMyInvoices(PagedResultRequestDto input)
        {
            var userId = AbpSession.UserId;
            var raw = (await _invoiceManager.GetAll())
                .Where(s=>s.UserId == userId);

            var result = raw.Select(s => new InvoiceListDto
            {
                CreateDate = s.CreationTime,
                Id = s.Id,
                PaymentStatus = s.PaymentStatus,
                ProductName = s.ProductName,
                SubTotal = s.SubTotal,
                ShopName = s.Seller.Name
            });

            return await GetPagedResult<InvoiceListDto>(result, input);
        }

        public async Task<PagedResultDto<InvoiceListDto>> GetAll(PagedResultRequestDto input)
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            var raw = (await _invoiceManager.GetAll())
                .Where(s => s.SellerId == seller.Id);

            var result = raw.Select(s => new InvoiceListDto
            {
                CreateDate = s.CreationTime,
                Id = s.Id,
                PaymentStatus = s.PaymentStatus,
                ProductName = s.ProductName,
                SubTotal = s.SubTotal,
                ShopName = s.Seller.Name
            });

            return await GetPagedResult<InvoiceListDto>(result, input);
        }

        public async Task<InvoiceDetailDto> Get(EntityDto<long> input)
        {
            var invoice = await _invoiceManager.Get(input.Id);
            return ObjectMapper.Map<InvoiceDetailDto>(invoice);
        }
    }
}
