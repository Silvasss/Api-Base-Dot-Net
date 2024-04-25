using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public partial class UserForLoginDto
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 6 a 8 caracteres", MinimumLength = 4)]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string Password { get; set; }
    }
}