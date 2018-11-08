using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class CadastroUsuario
    {
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(50, ErrorMessage = "O nome deve ter até 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail")]
        [MaxLength(50, ErrorMessage = "O e-mail deve ter até 50 caracteres")]
        [EmailAddress(ErrorMessage = "Entre com um E-mail válido!")]
        public string Email{get;set;}

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6" + "caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6" + "caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a" + "confirmação não estão iguais")]
        public string ConfirmacaoSenha { get; set; }
    }
}