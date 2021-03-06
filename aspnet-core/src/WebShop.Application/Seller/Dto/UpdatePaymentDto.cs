﻿using System.Collections.Generic;
using WebShop.Domain.Seller;

namespace WebShop.Seller.Dto
{
    public class UpdatePaymentDto
    {
        public PaymentOption SellerPaymentOption { get; set; }
        public List<KeyValuePair<string, string>> Payload { get; set; }
    }
}
