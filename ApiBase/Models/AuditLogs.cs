namespace ApiBase.Models
{
    public partial class AuditLogs
    {
        public int AuditLog_Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set;}
        public Auth? Auth { get; set; }
        public string Auth_Usuario { get; set; } = string.Empty;
    }
}