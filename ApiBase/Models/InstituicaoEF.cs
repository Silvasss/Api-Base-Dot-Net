using System.ComponentModel.DataAnnotations;

namespace ApiBase.Models
{
    public class InstituicaoEF
    {
        public int Instituicao_Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string PlusCode { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Auth? Auth { get; set; }
        public int Auth_Id { get; set; }
        public TipoConta? TipoConta { get; set; }
        public int Tipo_Conta_Id { get; set; }
        public IEnumerable<Curso>? Cursos { get; set; }
        public IEnumerable<Solicitacao>? Solicitacoes { get;}
    }
    public partial class Curso
    {
        public int Curso_Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public InstituicaoEF? Instituicao { get; set; }
        public int Instituicao_Id { get;set; }
        public IEnumerable<Graduacao>? Graduacoes { get; set;}
    }
    public partial class InstituicaoInsert : InstituicaoEF
    {
        [Required(ErrorMessage = "O nome do usuário é obrigatório")]
        [StringLength(32, ErrorMessage = "Tamanho entre 6 a 32 caracteres", MinimumLength = 4)]
        public required string Usuario { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo da senha 6 caracteres")]
        public required string Password { get; set; }
    }
}
