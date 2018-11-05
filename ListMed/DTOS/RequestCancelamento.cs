using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class RequestCancelamento
    {
        public int id_usuario_mobvendas { get; set; }
        public string email { get; set; }
        public List<Tipocancelamento> tipocancelamento { get; set; }
        public string maisfuncionalidade { get; set; }
        public string motivoCancelamento { get; set; }
    }
}