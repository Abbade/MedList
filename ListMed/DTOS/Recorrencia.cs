using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class Recorrencia
    {
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}