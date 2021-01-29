using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.PayIn.Dto;

namespace WebShop.PayIn
{
    public class PayInAppService : WebShopAppServiceBase
    {
        private readonly IRepository<Domain.Cash.PayIn, long> _payInRepository;
        private readonly IRepository<Domain.Seller.Seller, long> _sellerRepository;
        public PayInAppService(

            IRepository<Domain.Cash.PayIn, long> payInRepository,
            IRepository<Domain.Seller.Seller, long> sellerRepository
        )
        {
            _payInRepository = payInRepository;
            _sellerRepository = sellerRepository;
        }

        public async Task AddCredit(AddCreditDto input)
        {

            var seller = await SellerManager.GetSellerById(input.TargetId);
            var payIn = Domain.Cash.PayIn.MakePayIn(seller.Id, input.Money);
            seller.Credit += payIn.Credit;
            await _payInRepository.InsertAsync(payIn);
            await _sellerRepository.UpdateAsync(seller);
        }

        public async Task<PagedResultDto<CreditHistory>> GetAll( PagedResultRequestDto input)
        {
            var data = _payInRepository
                .GetAllIncluding(s => s.Target)
                .Select(s=> new CreditHistory
                {
                    Credit = s.Credit,
                    Date = s.CreationTime,
                    Id = s.Id,
                    Money = s.Money,
                    SellerName = s.Target.Name
                }
                )
                ;

            return await GetPagedResult(data, input);
        }
    }
}
