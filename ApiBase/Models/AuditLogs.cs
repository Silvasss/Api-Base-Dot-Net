namespace ApiBase.Models
{
    public partial class AuditLogs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InstituicaoId { get; set; }
        public required string ActivityType { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedDate { get; set;}
    }
}
