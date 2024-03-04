namespace ApiBase.Models
{
    public class GraduacaoCompleto
    {
        public int GraduacaoId { get; set; }
        public int UserId { get; set; }
        public required string Instituicao { get; set; }
        public required string Curso { get; set; }
        public required string Situacao { get; set; }
    }
}
