using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Cidade")]
    public class Cidade
    {
        public Cidade()
        {
            Clinicas = new List<Clinica>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "A descrição não pode ultrapassar de 200 caracteres!")]
        public string Descricao { get; set; }

        public int IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public virtual Estado Estado { get; set; }

        public virtual List<Clinica> Clinicas { get; set; }
    }
}