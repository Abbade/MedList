using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Avaliacao")]
    public class Avaliacao
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 5)]
        public int? nota { get; set; }

        [StringLength(275)]
        public string comentario { get; set; }

        public int IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public virtual Clinica Clinica { get; set; }
    }
}