using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class RequisicaoPagamento
    {
        public string id_usuario_mobvendas { get; set; }
        public string name { get; set; }
        public string cardnumber { get; set; }
        public string expirationdate { get; set; }
        public string brand { get; set; }
        public string securityCode { get; set; }
        public string amount { get; set; }
    }
}