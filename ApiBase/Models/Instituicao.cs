using System.ComponentModel.DataAnnotations;

namespace ApiBase.Models
{
    public partial class InstituicaoModel
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string PlusCode { get; set; }
    }    
    public partial class InstituicaoInsert : InstituicaoModel
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 6 a 32 caracteres", MinimumLength = 4)]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string Password { get; set; }
    }
    public partial class SolicitaCurso
    {
        public int Id { get; set; }
        public bool Situacao { get; set; }
        public string Explicacao { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Instituicao_Id { get; set; }
        public int User_Id { get; set; }
        public int Curso_Id { get; set; }
    }
    public partial class Curso
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public bool IsActive { get; set; }
    }
}
