using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Especialidade")]
    public class Especialidade
    {
        public Especialidade()
        {
            Clinicas = new List<Clinica>();
            AmostraClinicas = new List<AmostraClinica>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string descricao { get; set; }

        public virtual List<Clinica> Clinicas { get; set; }

        public virtual List<AmostraClinica> AmostraClinicas { get; set; }
    }
}