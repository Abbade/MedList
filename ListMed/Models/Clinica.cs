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
        public Clinica()
        {
            Localidades = new List<Localidade>();
            Servicos = new List<Servico>();
            Especialidades = new List<Especialidade>();
        }
        [Key]
        public int Id { get; set; }

        public string PlaceID { get; set; }

        [StringLength(180)]
        public string NomeFantasia { get; set; }

        [StringLength(180)]
        public string TituloSite { get; set; }


        public string Snippet { get; set; }

        [StringLength(80)]
        public string LinkSite { get; set; }


        public string Lt { get; set; }

        public string Lg { get; set; }

        public string EnderecoFormatado { get; set; }

        public double avaliacao { get; set; }

        public decimal? PrecoConsulta { get; set; }

        public decimal? PrecoExame { get; set; }

        public TimeSpan HoraAbertura { get; set; }

        public TimeSpan HoraFechamento { get; set; }


        [StringLength(17)]
        public string Telefone1 { get; set; }

        [StringLength(17)]
        public string Telefone2 { get; set; }

        public virtual List<Localidade> Localidades { get; set; }
        
        public virtual List<Servico> Servicos { get; set; }

        public virtual List<Especialidade> Especialidades { get; set; }

    }
}