using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListMed.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
            Avaliacoes = new List<Avaliacao>();
            Clinicas = new List<Clinica>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        public string email { get; set; }

        [Required(ErrorMessage = "nick é obrigatório!")]
        [MaxLength(50)]
        public string nick { get; set; }

        [Required(ErrorMessage = "senha é obrigatório!")]
        public string senha { get; set; }

        public byte[] Foto { get; set; }

        public virtual List<Avaliacao> Avaliacoes { get; set; }

        // clinicas que ele gosta
        public virtual List<Clinica> Clinicas { get; set; }
    }
}