using System.ComponentModel.DataAnnotations;

namespace ApiBase.Dtos
{
    public class UsuarioDto
    {
        public int Usuario_Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "País é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "PlusCode é obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho entre 4 a 150 caracteres", MinimumLength = 4)]
        public string PlusCode { get; set; } = string.Empty;

        [StringLength(650, ErrorMessage = "Tamanho máximo 650 caracteres")]
        public string SobreMin { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 150 caracteres", MinimumLength = 4)]
        public string CargoPrincipal { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "Tamanho máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "Tamanho máximo 150 caracteres")]
        public string PortfolioURL { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tempo de serviço é obrigatório")]
        public string Experiencia { get; set; } = string.Empty;
        public List<string>? ConfiguracoesConta { get; set; }
    }
    public class VisitanteDto
    {
        public int Usuario_Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "País é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "PlusCode é obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho entre 4 a 150 caracteres", MinimumLength = 4)]
        public string PlusCode { get; set; } = string.Empty;
        public IEnumerable<ExperienciaDto>? Experiencias { get; set; }
        public IEnumerable<GraduacaoDto>? Graduacoes { get; set; }
    }
    public partial class ExperienciaDto
    {
        public int Experiencia_Id { get; set; }

        [Required(ErrorMessage = "Setor é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Setor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Empresa é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Empresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "PlusCode é obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho entre 4 a 150 caracteres", MinimumLength = 4)]
        public string PlusCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vinculo é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 3 a 50 caracteres", MinimumLength = 3)]
        public string Vinculo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Função é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 3 a 50 caracteres", MinimumLength = 3)]
        public string Funcao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Responsabilidade é obrigatório")]
        [StringLength(150, ErrorMessage = "Tamanho entre 3 a 50 caracteres", MinimumLength = 3)]
        public string Responsabilidade { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
    }
    public partial class GraduacaoDto
    {
        public int Graduacao_Id { get; set; }

        [Required(ErrorMessage = "Situacao é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Situacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "Data de início é obrigatório")]
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int Curso_Id { get; set; }
        public string? CursoNome { get; set; }
        public int InstituicaoId { get; set; }
        public string? InstituicaoNome { get; set; }
    }
    public partial class SolicitacaoDto
    {
        public int Solicitacao_Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(50, ErrorMessage = "Tamanho entre 4 a 50 caracteres", MinimumLength = 4)]
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public partial class ListaInfos
    {
        public required IEnumerable<ListaInstituicaoDto> ListaInstituicoes { get; set; }
        public required IEnumerable<GraduacaoDto> Graduacoes { get; set; }
    }
    public partial class UsuarioIndexDto : UsuarioDto
    {
        public required IEnumerable<GraduacaoDto> Educacao { get; set; }
        public required IEnumerable<ExperienciaDto> Emprego { get; set; }
    }
}
