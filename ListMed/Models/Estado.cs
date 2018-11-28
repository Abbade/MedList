using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Estado")]
    public class Estado
    {
        public Estado()
        {
            Clinicas = new List<Clinica>();
            Cidades = new List<Cidade>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "A descrição não pode ultrapassar de 200 caracteres!")]
        public string Nome { get; set; }

        [MaxLength(2, ErrorMessage = "Somente 2 caracteres")]
        public string CodigoUf { get; set; }

        public string Uf { get; set; }

        public int Regiao { get; set; }


        [ForeignKey("Regiao")]
        public virtual Regiao RegiaoDoEstado { get; set; }
        public virtual List<Clinica> Clinicas { get; set; }
        public virtual List<Cidade> Cidades { get; set; }
    }
}