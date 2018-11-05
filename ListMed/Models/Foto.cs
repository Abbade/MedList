using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Foto")]
    public class Foto
    {
        [Key]
        public int Id { get; set; }

        [StringLength(120)]
        public string Titulo { get; set; }

        public string URL { get; set; }

        public int? IdClinica { get; set; }

        public double? altura { get; set; }

        public double? largura { get; set; }

        [ForeignKey("IdClinica")]
        public virtual Clinica Clinica { get; set; }
    }
}