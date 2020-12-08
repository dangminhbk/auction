using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WebShop.Auction.Dto;
using WebShop.Domain.Auction;
using WebShop.DtoBase;

namespace WebShop.Auction
{
    public class AuctionAppService : WebShopAppServiceBase
    {
        private readonly IAuctionManager _auctionManager;
        public AuctionAppService(IAuctionManager auctionManager)
        {
            _auctionManager = auctionManager;
        }
        public async Task Create(CreateAuctionDto input)
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            await _auctionManager.CreateAuction(
                  input.ProductId,
                  input.InitPrice,
                  input.MinAcceptPrice,
                  input.StartDate,
                  input.EndDate,
                  seller.Id
            );
        }

        public async Task Delete(EntityDto input)
        {

        }

        [HttpGet]
        public async Task<PagedResultDto<AuctionListDto>> ListActiveAuction(PagedSearchDto input)
        {
            DateTime now = DateTime.UtcNow;
            IQueryable<Domain.Auction.Auction> auctions = (await _auctionManager
                .GetAll())
                .Where(s => s.EndDate > now)
                .Where(s => s.StartDate < now)
                .WhereIf(!input.Keyword.IsNullOrEmpty(), s => s.Product.Name.Contains(input.Keyword))
                .OrderByDescending(s => s.EndDate);

            if (input.CategoryIds?.Length > 0)
            {
                auctions = auctions.WhereIf(
                    input.CategoryIds?.Length > 0,
                    s => s.Product.ProductCategories
                                    .Any(i => input.CategoryIds.Contains(i.CategoryId)));
            }

            auctions = auctions.WhereIf(
                    input.BrandId.HasValue,
                    s => s.Product.BrandId == input.BrandId);

            auctions = auctions.WhereIf(
                    input.MinPrice.HasValue,
                    s => s.CurrentPrice >= input.MinPrice);

            auctions = auctions.WhereIf(
                    input.MaxPrice.HasValue,
                    s => s.CurrentPrice <= input.MaxPrice);

            auctions = auctions.WhereIf(
                    input.SellerId.HasValue,
                    s => s.SellerId == input.SellerId);

            IQueryable<AuctionListDto> results = auctions.Select(s => new AuctionListDto
            {
                StartTime = s.StartDate,
                Id = s.Id,
                ProductName = s.Product.Name,
                EndTime = s.EndDate,
                ProductImage = s.Product.CoverImage.Image.Url,
                CurrentPrice = s.CurrentPrice,
                NumberOfBid = s.NumberOfBid
            });

            return await GetPagedResult<AuctionListDto>(results, input);
        }

        public async Task<PagedResultDto<AuctionListDto>> GetAll(PagedResultRequestDto input)
        {
            IQueryable<Domain.Auction.Auction> auctions = await _auctionManager
                .GetAll();
            IQueryable<AuctionListDto> results = auctions.Select(s => new AuctionListDto
            {
                Id = s.Id,
                ProductName = s.Product.Name,
                EndTime = s.EndDate,
                ProductImage = s.Product.CoverImage.Image.Url,
                CurrentPrice = s.CurrentPrice,
                NumberOfBid = s.NumberOfBid
            });

            return await GetPagedResult<AuctionListDto>(results, input);
        }

        public async Task<PagedResultDto<AuctionListDto>> GetAllForSeller(PagedResultRequestDto input)
        {
            Domain.Seller.Seller seller = await GetCurrentSeller();
            IQueryable<Domain.Auction.Auction> auctions = (await _auctionManager
                .GetAll()
                ).Where(s => s.SellerId == seller.Id)
                .OrderByDescending(s => s.EndDate);
            IQueryable<AuctionListDto> results = auctions.Select(s => new AuctionListDto
            {
                Id = s.Id,
                ProductName = s.Product.Name,
                EndTime = s.EndDate,
                ProductImage = s.Product.CoverImage.Image.Url,
                CurrentPrice = s.CurrentPrice,
                NumberOfBid = s.NumberOfBid
            });

            return await GetPagedResult<AuctionListDto>(results, input);
        }
        public async Task<AuctionDto> Get(EntityDto input)
        {
            Domain.Auction.Auction item = await _auctionManager.GetDetail(input.Id);

            return new AuctionDto
            {
                Id = item.Id,
                EndDate = item.EndDate,
                InitPrice = item.InitPrice,
                MinAcceptPrice = item.MinAcceptPrice,
                ProductId = item.ProductId,
                StartDate = item.StartDate,
                CurrentPrice = item.CurrentPrice,
                NumberOfBids = item.NumberOfBid,
                LastBidTime = item.LastBidTime,
                UserName = item.Winner?.UserName,
                SellerId = item.SellerId
            };
        }

        public async Task<PagedResultDto<BidDto>> GetBidsByAuctions(GetBidsByAuctionRequestDto input)
        {
            IQueryable<Domain.Bid.Bid> bids = await _auctionManager.GetBids(input.AuctionId);
            IQueryable<BidDto> result = bids.Select(s => new BidDto
            {
                AuctionId = s.AuctionId,
                Id = s.Id,
                BidPrice = s.BidPrice,
                BidTime = s.BidTime,
                UserId = s.UserId,
                UserName = s.User.UserName
            });

            return await GetPagedResult<BidDto>(result, input);
        }
    }
}
