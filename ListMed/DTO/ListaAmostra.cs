using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class ListaAmostra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string NomeUsuario { get; set; }
    }
}