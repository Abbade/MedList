using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Servico")]
    public class Servico
    {
        public Servico()
        {
            Clinicas = new List<Clinica>();
            AmostraClinicas = new List<AmostraClinica>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(200, ErrorMessage = "Descrição até 200 caracteres!")]
        [Required]
        public string Descricao { get; set; }


        public virtual List<Clinica> Clinicas { get; set; }

        public virtual List<AmostraClinica> AmostraClinicas { get; set; }
    }
}