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

            Servicos = new List<Servico>();
            Especialidades = new List<Especialidade>();
            Avaliacoes = new List<Avaliacao>();
            Fotos = new List<Foto>();
            Usuarios = new List<Usuario>();
            TelefonesClinicas = new List<TelefonesClinica>();
        }
        [Key]
        public int Id { get; set; }

        public string PlaceID { get; set; }

        [StringLength(180)]
        public string NomeFantasia { get; set; }

        [StringLength(180)]
        public string TituloSite { get; set; }


        [StringLength(80)]
        public string LinkSite { get; set; }


        public string Lt { get; set; }

        public string Lg { get; set; }

        public string EnderecoFormatado { get; set; }

        [Range(0, 5)]
        public double avaliacao { get; set; }

        public decimal? PrecoConsulta { get; set; }

        public decimal? PrecoExame { get; set; }

        public TimeSpan HoraAbertura { get; set; }

        public TimeSpan HoraFechamento { get; set; }

        public int? IdEstado { get; set; }


        public int? IdCidade { get; set; }

        public int? IdBairro { get; set; }

        public virtual List<TelefonesClinica> TelefonesClinicas { get; set; }

        public virtual List<Avaliacao> Avaliacoes { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        [ForeignKey("IdCidade")]
        public virtual Cidade Cidade { get; set; }

        [ForeignKey("IdBairro")]
        public virtual Bairro Bairro { get; set; }
        
        public virtual List<Servico> Servicos { get; set; }

        public virtual List<Especialidade> Especialidades { get; set; }

        public virtual List<Foto> Fotos { get; set; }

        // usuarios que gostaram da clinica
        public virtual List<Usuario> Usuarios { get; set; }
    }
}