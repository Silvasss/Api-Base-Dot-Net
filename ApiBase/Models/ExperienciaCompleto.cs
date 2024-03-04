namespace ApiBase.Models
{
    public class ExperienciaCompleto
    {
        public int ExperienciaId { get; set; }
        public int UserId { get; set; }
        public required string Setor { get; set; }
        public required string NomeEmpresa { get; set; }
        public required string Situacao { get; set; }
        public required string TipoEmprego { get; set; }
        public required string Pais { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
    }
}
