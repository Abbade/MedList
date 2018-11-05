using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class ModificaCartaoRequest
    {
       
        public int idUsuario { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public int Installments { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string SoftDescriptor { get; set; }
        public CreditCard CreditCard { get; set; }
    }

    //public class ModificaCartao
    //{

    //    public int idUsuario { get; set; }
    //    public string Type { get; set; }
    //    public string Amount { get; set; }
    //    public int Installments { get; set; }
    //    public string Country { get; set; }
    //    public string Currency { get; set; }
    //    public string SoftDescriptor { get; set; }
    //    public CreditCard CreditCard { get; set; }
    //}
}