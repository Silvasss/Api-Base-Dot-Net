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

    public class MetricasSistema
    {
        public required string Title { get; set; }
        public required string Count { get; set; }
        public double Percentage { get; set; }
        public required string Color { get; set; }
    }

    public class LogSerilog
    {
        public int Id { get; set; }
        public required string MessageTemplate { get; set; }
        public required string Level { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class AdminDashboard
    {
        public required List<MetricasSistema> AnalyticSistema { get; set; }
        public required List<LogSerilog> Logs { get; set; }
        public required List<int> FonteTrafico { get; set; }
    }
}