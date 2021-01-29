using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Configuration.Dto
{
    public class ChangeSettingDto
    {
        public BannerDto MainBanner { get; set; }
        public BannerDto SecondBanner { get; set; }
        public BannerDto ThirdBanner { get; set; }
        public PromoDto Promo { get; set; }
    }
}
