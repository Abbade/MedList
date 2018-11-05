using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_mobvendas.DTOS
{
    public class CancelaPagamento
    {
        public int id { get; set; }
        public int [] motivosDesistencia { get; set; }
        public string motivoDesistenciaTxt { get; set; }
        public DateTime dataDesistencia { get; set; }
    }
}