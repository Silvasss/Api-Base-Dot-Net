namespace ApiBase.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Pais { get; set; }
        public string PlusCode { get; set; } = string.Empty;
    }
    public partial class UserCompleto : User
    {
        public List<Experiencia>? Experiencia { get; set; }
        public List<Graduacao>? Graduacao { get; set; }
    }
    public partial class Graduacao
    {
        public int Id { get; set; }
        public int InstituicaoId { get; set; }
        public int CursoId { get; set; }
        public required string Situacao { get; set; }
        public required string CursoNome { get; set; }
        public required string InstituicaoNome { get; set; }
    }
    public partial class Experiencia
    {
        public int Id { get; set; }
        public required string Setor { get; set; }
        public required string Empresa { get; set; }
        public required string Situacao { get; set; }
        public required string Tipo { get; set; }
        public required string PlusCode { get; set; }
    }
}