using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class UsuarioSimples
    {
        public string email { get; set; }
        public int? id { get; set; }
        public string nome { get; set; }
        public DateTime? dataProxPag { get; set; }
    }
}