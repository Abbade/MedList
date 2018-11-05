
using System.ComponentModel.DataAnnotations;

namespace api_mobvendas.DTOS
{
    public class CadastroUsuarioAPI
    {
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(255, ErrorMessage = "O nome deve ter até 255 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(45, ErrorMessage = "O login deve ter até 45 caracteres")]
        public string Login { get; set; }

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