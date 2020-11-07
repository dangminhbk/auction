using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Auction
{
    public class AuctionHub: AbpHubBase, ITransientDependency
    {
    }
}
