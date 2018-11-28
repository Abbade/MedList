using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Regiao")]
    public class Regiao
    {
        public Regiao()
        {
            Estados = new List<Estado>();
        }
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }

        public virtual List<Estado> Estados { get; set; }
    }
}