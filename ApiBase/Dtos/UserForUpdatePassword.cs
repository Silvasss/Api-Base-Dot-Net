using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public class UserForUpdatePassword
    {
        [Required(ErrorMessage = "A senha atual é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string PasswordAtual { get; set; }

        [Required(ErrorMessage = "A nova senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string PasswordNova { get; set; }

        [Required(ErrorMessage = "A confirmação da senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        [Compare("PasswordNova", ErrorMessage = "Senhas não são iguais")]
        public required string PasswordConfirmacao { get; set;}
    }
}
