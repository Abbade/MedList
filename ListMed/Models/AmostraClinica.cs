using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("AmostraClinica")]
    public class AmostraClinica
    {
        public AmostraClinica()
        {

            Servicos = new List<Servico>();
            Especialidades = new List<Especialidade>();
            Fotos = new List<Foto>();
            TelefonesClinicas = new List<TelefonesClinica>();
        }
        [Key]
        public int Id { get; set; }

        public string PlaceID { get; set; }

        [StringLength(180)]
        public string NomeFantasia { get; set; }



        [StringLength(80)]
        public string LinkSite { get; set; }


        public string Lt { get; set; }

        public string Lg { get; set; }

        public string EnderecoFormatado { get; set; }

        [Range(0, 5)]
        public double? avaliacao { get; set; }

        public decimal? PrecoConsulta { get; set; }

        public decimal? PrecoExame { get; set; }

        public TimeSpan? HoraAbertura { get; set; }

        public TimeSpan? HoraFechamento { get; set; }

        public int IdEstado { get; set; }

        public int Pontos { get; set; }

        public int? IdCidade { get; set; }

        public Nullable<int> IdBairro { get; set; }

        public int IdUsuario { get; set; }

        public bool Ativo { get; set; } = true;

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        public virtual List<TelefonesClinica> TelefonesClinicas { get; set; }


        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("IdCidade")]
        public virtual Cidade Cidade { get; set; }

        [ForeignKey("IdBairro")]
        public virtual Bairro Bairro { get; set; }

        public virtual List<Servico> Servicos { get; set; }

        public virtual List<Especialidade> Especialidades { get; set; }

        public virtual List<Foto> Fotos { get; set; }

    }
}