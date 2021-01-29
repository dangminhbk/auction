using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.PayIn.Dto
{
    public class AddCreditDto
    {
        public decimal Money { get; set; }
        public long TargetId { get; set; }
    }
}
