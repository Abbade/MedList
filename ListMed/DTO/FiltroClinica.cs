using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class FiltroClinica
    {
        public FiltroClinica()
        {
            especialidades = new List<int>();
        }
        public string localidade { get; set; }
        public decimal preco { get; set; }
        public int servico { get; set; }
        public List<int> especialidades { get; set; }
    }
}