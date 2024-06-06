using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public partial class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 4 a 32 caracteres", MinimumLength = 4)]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [StringLength(16, ErrorMessage = "Tamanho entre 4 a 16 caracteres", MinimumLength = 4)]
        public string Password { get; set; } = string.Empty;
    }
}