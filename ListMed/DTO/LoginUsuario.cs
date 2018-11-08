using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListMed.DTO
{
    public class LoginUsuario
    {
        [Required(ErrorMessage = "Informe seu e-mail")]
        [MaxLength(50, ErrorMessage = "O e-mail deve ter até 50 caracteres")]
        
        [EmailAddress(ErrorMessage = "Entre com um E-mail válido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6" + "caracteres")]
        public string Senha { get; set; }

        [HiddenInput]
        public string UrlRetorno { get; set; }
    }
}