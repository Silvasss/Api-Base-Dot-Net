namespace ApiBase.Models
{
    public partial class Usuario
    {
        public int Usuario_Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string PlusCode { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Auth? Auth { get; set; }
        public int Auth_Id { get; set; }
        public int Tipo_Conta_Id { get; set; }
        public IEnumerable<Experiencia>? Experiencias { get; set; }
        public IEnumerable<Graduacao>? graduacoes { get; set; }
    }
    public partial class Experiencia
    {
        public int Experiencia_Id { get; set; }
        public string Setor { get; set; } = string.Empty;
        public string Empresa { get; set; } = string.Empty;
        public string PlusCode { get; set; } = string.Empty;
        public string Vinculo { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Usuario? Usuario { get; set; }
        public int Usuario_Id { get; set; }
    }
    public partial class Graduacao
    {
        public int Graduacao_Id { get; set; }
        public string? Situacao { get; set; }
        public int Curso_Id { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Usuario? Usuario { get; set; }
        public int Usuario_Id { get; set; }
        public Solicitacao? Solicitacao { get; set; }
        public int InstituicaoId { get; set; }
    }
    public partial class Solicitacao
    {
        public int Solicitacao_Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int Instituicao_Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Graduacao? Graduacao { get; set; }
        public int Graduacao_Id { get; set; }        
    }
}
