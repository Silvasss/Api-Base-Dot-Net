namespace ApiBase.Models
{
    public partial class AuditLogs
    {
        public int AuditLog_Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set;}
        public string Auth_Usuario { get; set; } = string.Empty;
    }
    public partial class SerilogDb
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string MessageTemplate { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; } = string.Empty;
        public string Properties { get; set; } = string.Empty;
    }
}