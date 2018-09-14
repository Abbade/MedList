using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Clinica")]
    public class Clinica
    {
        [Key]
        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        public string TituloSite { get; set; }

        public string Snippet { get; set; }

        public string LinkSite { get; set; }

        public int IdCidade { get; set; }
        // desnormalizacao
        public string DescricaoCidade { get; set; }

        public int IdBairro { get; set; }

        //desnormalizacao
        public string DescricaoBairro { get; set; }

        public int IdEstado { get; set; }

        // desnormalizacao
        public string UF { get; set; }

        public string Lt { get; set; }

        public string Lg { get; set; }

        public string EnderecoFormatado { get; set; }

        public decimal Preco { get; set; }

    }
}