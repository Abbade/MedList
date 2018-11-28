using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class AlterarPerfil
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public HttpPostedFileBase foto { get; set; }
        public string email { get; set; }
        public bool? temFoto { get; set; }
        public int pontos { get; set; }
    }
}