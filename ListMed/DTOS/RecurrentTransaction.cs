using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class RecurrentTransaction
    {
        public string PaymentId { get; set; }
        public int PaymentNumber { get; set; }
        public int TryNumber { get; set; }
    }
}