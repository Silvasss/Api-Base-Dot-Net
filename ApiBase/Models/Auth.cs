using Newtonsoft.Json;

namespace ApiBase.Models
{
    public partial class Auth
    {
        public int Auth_id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Usuario? UsuarioPerfil { get; set; }
        public InstituicaoEF? Instituicao { get; set; }
    }
}
