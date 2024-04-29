namespace ApiBase.Models
{
    public partial class TipoConta
    {
        public int Tipo_Conta_Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IEnumerable<Usuario>? Usuarios { get; set; }
        public IEnumerable<InstituicaoEF>? Instituicoes { get; set; }
    }
}
