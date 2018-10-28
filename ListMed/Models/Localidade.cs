using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Localidade")]
    public class Localidade
    {
        public Localidade()
        {
            Clinicas = new List<Clinica>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "A descrição não pode ultrapassar de 200 caracteres!")]
        public string Descricao { get; set; }

        // C [Cidade] | E [Estado] | B [bairro]
        [MaxLength(1, ErrorMessage = "Somente A, B ou E")]
        public string Tipo { get; set; }

        [MaxLength(2, ErrorMessage = "Somente 2 caracteres")]
        public string UF { get; set; }

        public virtual List<Clinica> Clinicas { get; set; }
    }
}