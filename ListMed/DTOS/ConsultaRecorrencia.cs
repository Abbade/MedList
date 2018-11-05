using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class ConsultaRecorrencia
    {
        public Customer Customer { get; set; }
        public RecurrentPayment RecurrentPayment { get; set; }
        
    }
}