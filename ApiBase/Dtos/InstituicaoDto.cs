using ApiBase.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public class InstituicaoDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Tamanho entre 4 a 100 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "PlusCode é obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho entre 4 a 150 caracteres", MinimumLength = 4)]
        public string PlusCode { get; set; } = string.Empty;
    }
    public partial class InstituicaoDtoCreate : InstituicaoDto
    {
        [Required(ErrorMessage = "Usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 4 a 32 caracteres", MinimumLength = 4)]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string Password { get; set; }
    }
    public class CursoDto
    {
        public int Curso_Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
    public class ListaInstituicaoDto
    {
        public int Instituicao_Id { get; set; }
        public required string Nome { get; set; }
        public IEnumerable<CursoDto> Cursos { get; set; }
    }
}
