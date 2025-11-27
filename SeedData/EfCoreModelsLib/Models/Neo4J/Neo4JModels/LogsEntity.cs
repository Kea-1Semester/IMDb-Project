namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class LogsEntity
    {
        public Guid LogId { get; set; }
        public string TableName { get; set; } = "";
        public string Command { get; set; } = "";
        public string NewValue { get; set; } = "";
        public string OldValue { get; set; } = "";
        public string ExecutedBy { get; set; } = "";
        public DateTime ExecutedAt { get; set; }
    }
}