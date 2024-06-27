namespace ApiBase.Models
{
    public partial class AuditLogs
    {
        public int AuditLog_Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Auth_Usuario { get; set; } = string.Empty;
    }
    public class SerilogEntry
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }
        public string? LogEvent { get; set; }
    }
}