using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("TelefonesClinica")]
    public class TelefonesClinica
    {
        [Key]
        public int Id { get; set; }

        [StringLength(17)]
        public string Numero { get; set; }

        public int? IdClinica { get; set; }

        [ForeignKey("IdClinica")]
        public virtual Clinica Clinica { get; set; }

        public int? IdAmostraClinica { get; set; }

        [ForeignKey("IdAmostraClinica")]
        public virtual AmostraClinica AmostraClinica { get; set; }

    }
}