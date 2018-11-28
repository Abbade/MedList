using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Bairro")]
    public class Bairro
    {
        public Bairro()
        {
            Clinicas = new List<Clinica>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "A descrição não pode ultrapassar de 200 caracteres!")]
        public string Nome { get; set; }


        [MaxLength(20)]
        public string Codigo { get; set; }

        [MaxLength(2)]
        public string Uf { get; set; }



        public virtual List<Clinica> Clinicas { get; set; }
    }
}