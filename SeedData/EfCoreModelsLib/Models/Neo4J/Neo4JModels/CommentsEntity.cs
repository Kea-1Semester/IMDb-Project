namespace EfCoreModelsLib.Models.Neo4J.Neo4JModels
{
    public partial class CommentsEntity
    {
        public Guid CommentId { get; set; }
        public string Comment { get; set; } = "";
    }
}