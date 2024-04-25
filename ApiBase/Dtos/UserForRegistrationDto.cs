using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public partial class UserForRegistrationDto
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(8, ErrorMessage = "Tamanho entre 6 a 8 caracteres", MinimumLength = 4)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public string Password { get; set; } = string.Empty;
    }
}